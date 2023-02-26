using OneOf;
using OneOf.Types;
using Quiz.QuestionStorage.Db.Models;

namespace Quiz.QuestionStorage.Services;

public interface IQuestionService
{
	Task<OneOf<Question, NotFound>> GetQuestionAsync(Guid id);

	Task<OneOf<T, NotFound>> GetFormulationAsync<T>(Guid questionId, CancellationToken cancellationToken) where T : QuestionFormulation;

	Task<OneOf<T, NotFound>> GetAnswerAsync<T>(Guid questionId, CancellationToken cancellationToken) where T : AnswerDefinition;
}