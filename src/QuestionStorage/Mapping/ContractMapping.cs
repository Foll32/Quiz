using Quiz.QuestionStorage.Contracts;
using Quiz.QuestionStorage.Results;

namespace Quiz.QuestionStorage;

internal static class ContractMapping
{
	public static ValidationFailureResponse MapToResponse(this ValidationError error)
	{
		return new ValidationFailureResponse(error.Errors.Select(e => new ValidationErrorItem(e.PropertyName, e.ErrorMessage)).ToArray());
	}
}