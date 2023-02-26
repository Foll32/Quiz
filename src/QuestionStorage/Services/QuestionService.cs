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
		switch (type)
		{
			case QuestionFormulationType.TextOnly:
				var formulation = await _context.TextOnlyFormulations.FirstOrDefaultAsync(f => f.QuestionId == questionId, cancellationToken);
				if (formulation is T textOnlyQuestionFormulation)
					return textOnlyQuestionFormulation;

				return new NotFound();

			default:
				return new ValidationError();
		}
	}

	public async Task<OneOf<T, NotFound, ValidationError>> GetAnswerAsync<T>(AnswerDefinitionType type, Guid questionId, CancellationToken cancellationToken)
		where T : AnswerDefinition
	{
		switch (type)
		{
			case AnswerDefinitionType.FreeText:
				var answerDefinition = await _context.FreeTextAnswerDefinitions.FirstOrDefaultAsync(f => f.QuestionId == questionId, cancellationToken);
				if (answerDefinition is T freeTextAnswerDefinition)
					return freeTextAnswerDefinition;

				return new NotFound();

			default:
				return new ValidationError();
		}
	}
}