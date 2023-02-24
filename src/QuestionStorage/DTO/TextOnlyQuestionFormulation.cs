using Quiz.Core.Abstractions;

namespace Quiz.QuestionStorage.DTO;

public class TextOnlyQuestionFormulation : QuestionFormulation
{
	public FormattedString Text { get; init; }
}