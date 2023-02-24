namespace Quiz.Core.Abstractions.Answers;

/// <summary>
/// Определение ответа со свободным текстовым вводом.
/// </summary>
public interface IFreeTextAnswerDefinition : IAnswerDefinition
{
	/// <summary>
	/// Правильный ответ.
	/// </summary>
	public FormattedString CorrectAnswer { get; }
	
	/// <summary>
	/// Дополнительные правильные ответы.
	/// </summary>
	/// <remarks>
	/// В каждом из дополнительных ответов запрещен символ \uFFFF.
	/// </remarks>
	public IReadOnlyCollection<FormattedString>? AdditionalAnswers { get; }
}