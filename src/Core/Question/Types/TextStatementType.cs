namespace Core.Question.Types;

/// <summary>
/// Постановка вопроса в виде простого текста.
/// </summary>
public class TextStatementType :
	StatementType
{
	public override Guid Id => new Guid("91158B67-4959-4B99-BB68-B6646031DB97");
	public override string Name => "Текстовый вопрос";
}