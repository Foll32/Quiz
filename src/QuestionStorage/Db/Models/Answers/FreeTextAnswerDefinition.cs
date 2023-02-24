using Quiz.Core.Abstractions;
using Quiz.Core.Abstractions.Answers;

namespace Quiz.QuestionStorage.Db.Models.Answers;

public class FreeTextAnswerDefinition : AnswerDefinition, IFreeTextAnswerDefinition
{
	public FreeTextAnswerDefinition()
	{
		Type = AnswerType.FreeText;
	}

	public FormattedString CorrectAnswer { get; init; }
	
	public IReadOnlyCollection<FormattedString>? AdditionalAnswers { get; init; }
}