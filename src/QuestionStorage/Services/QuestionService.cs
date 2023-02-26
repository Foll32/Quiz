using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;
using Quiz.Core.Abstractions;
using Quiz.QuestionStorage.Db;
using Quiz.QuestionStorage.Db.Models;
using Quiz.QuestionStorage.Results;

namespace Quiz.QuestionStorage.Services;

internal class QuestionService : IQuestionService
{
	private readonly Context _context;

	public QuestionService(Context context)
	{
		_context = context;
	}

	public async Task<OneOf<Question, NotFound>> GetQuestionAsync(Guid id)
	{
		var question = await _context.Questions.FirstOrDefaultAsync(q => q.Id == id);
		return question is null
			? new NotFound()
			: question;
	}

	public async Task<OneOf<T, NotFound, ValidationError>> GetFormulationAsync<T>(QuestionFormulationType type, Guid questionId, CancellationToken cancellationToken)
		where T : QuestionFormulation
	{
		IQueryable<QuestionFormulation>? dbSet = type switch
		{
			QuestionFormulationType.TextOnly => _context.TextOnlyFormulations,
			_ => null
		};
		
		if (dbSet is null)
			return new ValidationError();
		
		var formulation = await dbSet.FirstOrDefaultAsync(f => f.QuestionId == questionId, cancellationToken);
		if (formulation is T typedFormulation)
			return typedFormulation;

		return new NotFound();
		
	}

	public async Task<OneOf<T, NotFound, ValidationError>> GetAnswerAsync<T>(AnswerDefinitionType type, Guid questionId, CancellationToken cancellationToken)
		where T : AnswerDefinition
	{
		IQueryable<AnswerDefinition>? dbSet = type switch
		{
			AnswerDefinitionType.FreeText => _context.FreeTextAnswerDefinitions,
			AnswerDefinitionType.OneTextChoice => _context.OneTextChoiceAnswerDefinitions,
			_ => null
		};
		
		if (dbSet is null)
			return new ValidationError();
		
		var answerDefinition = await dbSet.FirstOrDefaultAsync(f => f.QuestionId == questionId, cancellationToken);
		if (answerDefinition is T typedAnswerDefinition)
			return typedAnswerDefinition;

		return new NotFound();
	}
}