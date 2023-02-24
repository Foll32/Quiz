using Quiz.Core.Abstractions;

namespace Quiz.QuestionStorage.Db.Models.Answers;

public abstract class AnswerDefinition : IAnswerDefinition
{
	public long Id { get; init; }

	public AnswerType Type { get; init; }
	
	public FormattedString? NotesForPlayers { get; init; }

	public Question Question { get; init; } = null!;
}