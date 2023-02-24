using AutoMapper;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;
using Quiz.Core.Abstractions;
using Quiz.QuestionStorage.Contracts;
using Quiz.QuestionStorage.Db;
using Quiz.QuestionStorage.Results;
using AnswerDefinition = Quiz.QuestionStorage.Contracts.AnswerDefinition;
using Question = Quiz.QuestionStorage.Contracts.Question;

namespace Quiz.QuestionStorage.Services;

internal class QuestionService : IQuestionService
{
	private readonly Context _context;
	private readonly IMapper _mapper;

	public QuestionService(Context context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}
	
	public async Task<OneOf<Question, NotFound>> GetQuestionAsync(Guid id)
	{
		var question = await _context.Questions.FirstOrDefaultAsync(q => q.Id == id);
		return question is null
			? new NotFound()
			: _mapper.Map<Question>(question);
	}

	public async Task<OneOf<QuestionFormulation, NotFound, ValidationError>> GetFormulationAsync(int type, Guid questionId, CancellationToken cancellationToken)
	{
		switch ((QuestionFormulationType)type)
		{
			case QuestionFormulationType.TextOnly:
				var formulation = await _context.TextOnlyFormulations.FirstOrDefaultAsync(f => f.QuestionId == questionId, cancellationToken);
				if (formulation is null)
					return new NotFound();
				
				return _mapper.Map<TextOnlyQuestionFormulation>(formulation);
			
			default:
				return new ValidationError(new[] {new ValidationFailure("Type", "Invalid question formulation type")});
		}
	}

	public async Task<OneOf<AnswerDefinition, NotFound, ValidationError>> GetAnswerAsync(int type, Guid questionId, CancellationToken cancellationToken)
	{
		switch ((AnswerDefinitionType)type)
		{
			case AnswerDefinitionType.FreeText:
				var answerDefinition = await _context.FreeTextAnswerDefinitions.FirstOrDefaultAsync(f => f.QuestionId == questionId, cancellationToken);
				if (answerDefinition is null)
					return new NotFound();
				
				return _mapper.Map<FreeTextAnswerDefinition>(answerDefinition);
			
			default:
				return new ValidationError(new[] {new ValidationFailure("Type", "Invalid answer definition type")});
		}
	}
}