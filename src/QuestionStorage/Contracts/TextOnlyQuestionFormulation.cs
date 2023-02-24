namespace Quiz.QuestionStorage.Contracts;

public class TextOnlyQuestionFormulation : QuestionFormulation
{
	public string Text { get; init; } = null!;
}