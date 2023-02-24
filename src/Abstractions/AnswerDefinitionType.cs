namespace Quiz.Core.Abstractions;

/// <summary>
/// Тип формулировки ответа.
/// </summary>
public enum AnswerDefinitionType
{
	/// <summary>
	/// Неопределенный тип ответа.
	/// </summary>
	None,
	
	/// <summary>
	/// Ответ вводится в свободном виде.
	/// </summary>
	FreeText,
	
	/// <summary>
	/// Один из предложенных текстовых вариантов.
	/// </summary>
	OneTextChoice
}