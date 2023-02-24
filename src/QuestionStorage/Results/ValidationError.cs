using FluentValidation.Results;

namespace Quiz.QuestionStorage.Results;

public record struct ValidationError(IEnumerable<ValidationFailure> Errors);