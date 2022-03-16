using Core.Question.Types;

namespace Core.Question;

/// <summary>
/// Формулировка вопроса.
/// </summary>
public interface IStatement
{
	IStatementType StatementType { get; }
}