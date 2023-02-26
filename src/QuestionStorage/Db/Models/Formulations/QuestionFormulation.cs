namespace Quiz.QuestionStorage.Db.Models;

public abstract class QuestionFormulation
{
	public Guid QuestionId { get; init; }

	public string? NotesForHost { get; init; }
}