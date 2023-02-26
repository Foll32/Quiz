using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;
using Quiz.QuestionStorage.Db;
using Quiz.QuestionStorage.Db.Models;
using Quiz.QuestionStorage.Grpc;
using Quiz.QuestionStorage.Results;
using Quiz.QuestionStorage.Validators;
using Question = Quiz.QuestionStorage.Db.Models.Question;

namespace Quiz.QuestionStorage.Services;

internal class QuestionService : IQuestionService
{
	private readonly Context _context;
	private readonly IMapper _mapper;
	private readonly NewQuestionValidator _newQuestionValidator = new NewQuestionValidator();

	public QuestionService(Context context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<OneOf<Question, NotFound>> GetQuestionAsync(Guid id, CancellationToken cancellationToken)
	{
		var question = await _context.Questions.FirstOrDefaultAsync(q => q.Id == id, cancellationToken);
		return question is null
			? new NotFound()
			: question;
	}

	public async Task<OneOf<T, NotFound>> GetFormulationAsync<T>(Guid questionId, CancellationToken cancellationToken)
		where T : QuestionFormulation
	{
		IQueryable<QuestionFormulation> dbSet = _context.Set<T>();

		var formulation = await dbSet.FirstOrDefaultAsync(f => f.QuestionId == questionId, cancellationToken);
		if (formulation is T typedFormulation)
			return typedFormulation;

		return new NotFound();
		
	}

	public async Task<OneOf<T, NotFound>> GetAnswerAsync<T>(Guid questionId, bool withAnswer, CancellationToken cancellationToken)
		where T : AnswerDefinition
	{
		IQueryable<AnswerDefinition> dbSet = _context.Set<T>();

		var answerDefinition = await dbSet.FirstOrDefaultAsync(f => f.QuestionId == questionId, cancellationToken);
		if (answerDefinition is not T typedAnswerDefinition)
			return new NotFound();
		
		if (!withAnswer)
			typedAnswerDefinition.ClearAnswer();
		
		return typedAnswerDefinition;
	}

	public async Task<OneOf<Guid, ValidationError>> AddQuestion(NewQuestionRequest newQuestion, CancellationToken cancellationToken)
	{
		var validationResult = await _newQuestionValidator.ValidateAsync(newQuestion, cancellationToken);
		if (!validationResult.IsValid)
			return new ValidationError();

		var questionId = Guid.NewGuid(); 
		
		var formulation = _mapper.Map<QuestionFormulation>(newQuestion);
		var answerDefinition = _mapper.Map<AnswerDefinition>(newQuestion);
		
		var question = new Question
		{
			Id = questionId,
			QuestionFormulationType = formulation.Type,
			AnswerDefinitionType = answerDefinition.Type
		};
		formulation.QuestionId = questionId;
		answerDefinition.QuestionId = questionId;

		_context.Questions.Add(question);
		_context.Entry(formulation).Context.Add(formulation);
		_context.Entry(answerDefinition).Context.Add(answerDefinition);

		await _context.SaveChangesAsync(cancellationToken);

		return questionId;
	}
}