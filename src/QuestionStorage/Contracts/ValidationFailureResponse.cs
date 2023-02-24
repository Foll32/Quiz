namespace Quiz.QuestionStorage.Contracts;

public record ValidationFailureResponse(IReadOnlyCollection<ValidationErrorItem> Errors);