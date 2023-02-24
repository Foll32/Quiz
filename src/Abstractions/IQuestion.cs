namespace Quiz.Core.Abstractions;

/// <summary>
/// Вопрос.
/// </summary>
public interface IQuestion
{
	/// <summary>
	/// Идентификатор вопроса.
	/// </summary>
	public Guid Id { get; }

	/// <summary>
	/// Формулировка вопроса.
	/// </summary>
	public IFormulation QuestionFormulation { get; }
	
	/// <summary>
	/// Формулировка правильного ответа.
	/// </summary>
	public IAnswerDefinition AnswerDefinition { get; }
}