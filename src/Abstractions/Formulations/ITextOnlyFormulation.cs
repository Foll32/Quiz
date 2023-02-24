namespace Quiz.Core.Abstractions.Formulations;

/// <summary>
/// Формулировка вопроса, состоящая только из текста.
/// </summary>
public interface ITextOnlyFormulation : IFormulation
{
	/// <summary>
	/// Текст формулировки вопроса.
	/// </summary>
	public FormattedString Text { get; }
}