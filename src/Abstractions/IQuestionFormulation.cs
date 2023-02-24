namespace Quiz.Core.Abstractions;

/// <summary>
/// Формулировка вопроса.
/// </summary>
public interface IQuestionFormulation
{
	/// <summary>
	/// Тип формулировки вопроса.
	/// </summary>
	public FormulationType Type { get; }
	
	/// <summary>
	/// Заметки для ведущего.
	/// </summary>
	public FormattedString? NotesForHost { get; }
}