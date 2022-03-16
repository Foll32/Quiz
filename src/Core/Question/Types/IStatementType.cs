namespace Core.Question.Types;

/// <summary>
/// Тип постановки вопроса.
/// </summary>
public interface IStatementType
{
	Guid Id { get; }
	
	string Name { get; }
}