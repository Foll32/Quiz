namespace Quiz.QuestionStorage.Db.Models;

public class FreeTextAnswerDefinition : AnswerDefinition
{
	public string CorrectAnswer { get; init; } = null!;

	public string? AdditionalAnswers { get; init; }
}