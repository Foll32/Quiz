using Quiz.Core.Abstractions;

namespace Quiz.QuestionStorage.DTO;

public class FreeTextAnswerDefinition : AnswerDefinition
{
	public FormattedString CorrectAnswer { get; init; }
	
	public IReadOnlyCollection<FormattedString>? AdditionalAnswers { get; init; }
}