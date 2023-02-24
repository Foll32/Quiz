namespace Quiz.Core.Abstractions.Answers;

/// <summary>
/// Определение ответа с выбором одного из предложенных текстовых вариантов.
/// </summary>
public interface IOneTextChoiceAnswerDefinition : IAnswerDefinition
{
	/// <summary>
	/// Список вариантов ответов.
	/// </summary>
	public IReadOnlyList<FormattedString> Answers { get; }
	
	/// <summary>
	/// Индекс правильного варианта ответа.
	/// </summary>
	public int CorrectAnswerIndex { get; }
}