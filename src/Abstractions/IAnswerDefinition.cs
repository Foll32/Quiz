namespace Quiz.Core.Abstractions;

/// <summary>
/// Определение правильного ответа.
/// </summary>
public interface IAnswerDefinition
{
	/// <summary>
	/// Тип правильного ответа.
	/// </summary>
	public AnswerType Type { get; }

	/// <summary>
	/// Заметки для игроков.
	/// </summary>
	public FormattedString? NotesForPlayers { get; }
}