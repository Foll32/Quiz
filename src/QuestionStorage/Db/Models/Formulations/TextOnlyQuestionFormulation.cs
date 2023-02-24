namespace Quiz.QuestionStorage.Db.Models;

public class TextOnlyQuestionFormulation : QuestionFormulation
{
	public string Text { get; init; } = null!;
}