using Quiz.Core.Abstractions;

namespace Quiz.CommonModels.Answers;

public abstract class AnswerDefinition : IAnswerDefinition
{
	public AnswerType Type { get; init; }
	
	public FormattedString? NotesForHost { get; init; }
	
	public FormattedString? NotesForPlayers { get; init; }
}