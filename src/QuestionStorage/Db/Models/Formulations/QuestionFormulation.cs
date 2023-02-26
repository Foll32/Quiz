using Quiz.Core.Abstractions;

namespace Quiz.QuestionStorage.Db.Models;

public abstract class QuestionFormulation
{
	public Guid QuestionId { get; set; }

	public string? NotesForHost { get; set; }

	public abstract QuestionFormulationType Type { get; }
}