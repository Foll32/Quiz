using FluentValidation;
using Quiz.QuestionStorage.Grpc;

namespace Quiz.QuestionStorage.Validators;

public class NewQuestionValidator : AbstractValidator<NewQuestion>
{
	public NewQuestionValidator()
	{
		RuleFor(q => q.AnswerCase).Must(BeDefinedOneOf);
		RuleFor(q => q.FormulationCase).Must(BeDefinedOneOf);
	}

	private bool BeDefinedOneOf(NewQuestion.AnswerOneofCase oneOfCase) =>
		oneOfCase != NewQuestion.AnswerOneofCase.None;

	private bool BeDefinedOneOf(NewQuestion.FormulationOneofCase oneOfCase) =>
		oneOfCase != NewQuestion.FormulationOneofCase.None;
}