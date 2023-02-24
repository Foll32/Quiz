namespace Quiz.QuestionStorage.Db.Models;

public abstract class AnswerDefinition
{
	public Guid QuestionId { get; init; }

	public string? NotesForPlayers { get; init; }
}