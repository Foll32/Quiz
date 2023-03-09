using FluentValidation;
using Quiz.QuestionStorage.Grpc;

namespace Quiz.QuestionStorage.Validators;

public class NewQuestionRequestValidator : AbstractValidator<NewQuestionRequest>
{
	public NewQuestionRequestValidator()
	{
		RuleFor(q => q.AnswerCase).Must(BeDefinedOneOf);
		RuleFor(q => q.FormulationCase).Must(BeDefinedOneOf);
	}

	private bool BeDefinedOneOf(NewQuestionRequest.AnswerOneofCase oneOfCase)
	{
		return oneOfCase != NewQuestionRequest.AnswerOneofCase.None;
	}

	private bool BeDefinedOneOf(NewQuestionRequest.FormulationOneofCase oneOfCase)
	{
		return oneOfCase != NewQuestionRequest.FormulationOneofCase.None;
	}
}