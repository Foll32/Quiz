namespace Core.Question.Types;

public class MultipleAnswerVariantsOptionsType :
	AnswerVariantsType
{
	public override Guid Id => Guid.Parse("5975D5D5-5477-4151-B37C-85F94AE903B7");
	public override string Name => "Ответ с несколькими допустимыми вариантами выбора из нескольких представленных";
}