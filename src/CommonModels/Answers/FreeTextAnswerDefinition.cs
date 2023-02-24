using Quiz.Core.Abstractions;
using Quiz.Core.Abstractions.Answers;

namespace Quiz.CommonModels.Answers;

public class FreeTextAnswerDefinition : AnswerDefinition, IFreeTextAnswerDefinition
{
	public const AnswerType FixedAnswerType = AnswerType.FreeText;

	public FreeTextAnswerDefinition(FormattedString correctAnswer)
	{
		Type = FixedAnswerType;
		CorrectAnswer = correctAnswer;
	}

	public FormattedString CorrectAnswer { get; }
	
	public IReadOnlyCollection<FormattedString>? AdditionalAnswers { get; init; }
}