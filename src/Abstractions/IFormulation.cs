namespace Quiz.Core.Abstractions;

/// <summary>
/// Формулировка вопроса.
/// </summary>
public interface IFormulation
{
	/// <summary>
	/// Тип формулировки вопроса.
	/// </summary>
	public FormulationType Type { get; }
}