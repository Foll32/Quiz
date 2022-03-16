namespace Core.Question.Types;

public class TextInputAnswerVariantsType :
	IAnswerVariantsType
{
	public Guid Id => Guid.Parse("AF190ACB-24B1-4AFE-A887-5A74D6922269");
	public string Name => "Ответ с единственным вариантом, который надо ввести";
}