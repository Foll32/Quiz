using OneOf;
using OneOf.Types;
using Quiz.QuestionStorage.Db.Models;
using Quiz.QuestionStorage.Grpc;
using Quiz.QuestionStorage.Results;
using Question = Quiz.QuestionStorage.Db.Models.Question;

namespace Quiz.QuestionStorage.Services;

public interface IQuestionService
{
	Task<IReadOnlyCollection<Question>> GetQuestionsAsync(IEnumerable<Guid> questionIds, CancellationToken cancellationToken);

	Task<OneOf<T, NotFound>> GetFormulationAsync<T>(Guid questionId, CancellationToken cancellationToken) where T : QuestionFormulation;

	Task<OneOf<T, NotFound>> GetAnswerAsync<T>(Guid questionId, bool withAnswer, CancellationToken cancellationToken) where T : AnswerDefinition;

	Task<OneOf<Guid, ValidationError>> AddQuestion(NewQuestionRequest newQuestion, CancellationToken cancellationToken);
}