using OneOf;
using OneOf.Types;
using Quiz.QuestionStorage.Db.Models;
using Quiz.QuestionStorage.Results;

namespace Quiz.QuestionStorage.Services;

public interface IQuestionService
{
	Task<OneOf<Question, NotFound>> GetQuestionAsync(Guid id, CancellationToken cancellationToken);

	Task<OneOf<T, NotFound>> GetFormulationAsync<T>(Guid questionId, CancellationToken cancellationToken) where T : QuestionFormulation;

	Task<OneOf<T, NotFound>> GetAnswerAsync<T>(Guid questionId, CancellationToken cancellationToken) where T : AnswerDefinition;

	Task<OneOf<Guid, ValidationError>> AddQuestion(Grpc.NewQuestion newQuestion, CancellationToken cancellationToken);
}