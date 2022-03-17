namespace Core.Question.Types;

public class NoAnswerVariantsType :
	IAnswerVariantsType
{
	public Guid Id => Guid.Parse("22779F56-36F9-415F-B7C9-DE7C6CCD8437");
	public string Name => "Без вариантов с единственным текстовым ответом";
}