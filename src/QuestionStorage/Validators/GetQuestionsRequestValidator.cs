using FluentValidation;
using Quiz.QuestionStorage.Grpc;

namespace Quiz.QuestionStorage.Validators;

public class GetQuestionsRequestValidator : AbstractValidator<GetQuestionsRequest>
{
	internal const int QuestionCountLimit = 20; 
	
	public GetQuestionsRequestValidator()
	{
		RuleFor(q => q.Id).Must(q => q.Count > 0).WithMessage("Empty question ids list");
		RuleFor(q => q.Id).Must(q => q.Count <= QuestionCountLimit).WithMessage("Too many question ids");
	}
}