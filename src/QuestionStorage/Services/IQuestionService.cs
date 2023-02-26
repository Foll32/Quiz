using OneOf;
using OneOf.Types;
using Quiz.Core.Abstractions;
using Quiz.QuestionStorage.Db.Models;
using Quiz.QuestionStorage.Results;

namespace Quiz.QuestionStorage.Services;

public interface IQuestionService
{
	Task<OneOf<Question, NotFound>> GetQuestionAsync(Guid id);

	Task<OneOf<T, NotFound, ValidationError>> GetFormulationAsync<T>(QuestionFormulationType type, Guid questionId, CancellationToken cancellationToken) where T : QuestionFormulation;
	
	Task<OneOf<T, NotFound, ValidationError>> GetAnswerAsync<T>(AnswerDefinitionType type, Guid questionId, CancellationToken cancellationToken) where T : AnswerDefinition;
}