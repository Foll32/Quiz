namespace Core.Question;

/// <summary>
/// Интерфейс вопроса.
/// </summary>
public interface IQuestion
{
	/// <summary>
	/// Идентификатор вопроса.
	/// </summary>
	Guid Id { get; }
	
	/// <summary>
	/// Постановка вопроса.
	/// </summary>
	IStatement Statement { get; }
	
	/// <summary>
	/// Варианты ответа.
	/// </summary>
	IAnswerVariants AnswerVariants { get; }
}