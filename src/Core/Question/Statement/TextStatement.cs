using Core.Question.Types;

namespace Core.Question.Statement;

public class TextStatement :
	IStatement
{
	public IStatementType StatementType { get; }
	
	public string StatementText { get; }

	public TextStatement(string statementText)
	{
		StatementType = DefaultTypes.StatementTypes[typeof(TextStatementType)];
		StatementText = statementText;
	}
}