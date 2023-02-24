using OneOf;
using OneOf.Types;
using Quiz.QuestionStorage.Contracts;
using Quiz.QuestionStorage.Results;

namespace Quiz.QuestionStorage.Services;

public interface IQuestionService
{
	Task<OneOf<Question, NotFound>> GetQuestionAsync(Guid id);

	Task<OneOf<QuestionFormulation, NotFound, ValidationError>> GetFormulationAsync(int type, Guid questionId, CancellationToken cancellationToken);
	
	Task<OneOf<AnswerDefinition, NotFound, ValidationError>> GetAnswerAsync(int type, Guid questionId, CancellationToken cancellationToken);
}