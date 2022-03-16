namespace Core.Question.Types;

public abstract class StatementType :
	IStatementType
{
	public abstract Guid Id { get; }

	public abstract string Name { get; }
}