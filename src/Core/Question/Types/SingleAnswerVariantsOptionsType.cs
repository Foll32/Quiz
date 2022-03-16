namespace Core.Question.Types;

public class SingleAnswerVariantsOptionsType :
	AnswerVariantsType
{
	public override Guid Id => Guid.Parse("E8AE1F12-324E-4794-A30E-F5F8B3063F2A");
	public override string Name => "Ответ с единственным допустимым вариантом выбора из нескольких представленных";
}