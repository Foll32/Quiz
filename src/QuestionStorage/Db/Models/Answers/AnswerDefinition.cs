using Quiz.Core.Abstractions;

namespace Quiz.QuestionStorage.Db.Models;

public abstract class AnswerDefinition
{
	public abstract AnswerDefinitionType Type { get; }

	public Guid QuestionId { get; set; }

	public string? NotesForPlayers { get; set; }
}