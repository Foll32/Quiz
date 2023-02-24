namespace Quiz.Core.Abstractions;

/// <summary>
/// Тип формулировки вопроса.
/// </summary>
public enum QuestionFormulationType
{
	/// <summary>
	/// Неопределенная формулировка вопроса.
	/// </summary>
	None,
	
	/// <summary>
	/// Формулировка вопроса, состоящая лишь из текста.
	/// </summary>
	TextOnly,
	
	/// <summary>
	/// Формулировка вопроса, состоящая из текста и изображения.
	/// </summary>
	TextWithImage
}