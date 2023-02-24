using Quiz.Core.Abstractions;

namespace Quiz.QuestionStorage.Db.Models.Formulations;

public abstract class QuestionFormulation : IQuestionFormulation
{
	public long Id { get; init; }
	
	public FormulationType Type { get; init; }
	
	public FormattedString? NotesForHost { get; init; }

	public Question Question { get; init; } = null!;
}