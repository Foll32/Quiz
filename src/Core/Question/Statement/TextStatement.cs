using Core.Question.Types;

namespace Core.Question.Statement;

public class TextStatement :
	IStatement
{
	public IStatementType StatementType { get; }
	
	public string StatementText { get; }

	public TextStatement(TextStatementType textStatementType, string statementText)
	{
		StatementType = textStatementType;
		StatementText = statementText;
	}
}