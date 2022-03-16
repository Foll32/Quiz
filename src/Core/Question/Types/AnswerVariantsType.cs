namespace Core.Question.Types;

public abstract class AnswerVariantsType :
	IAnswerVariantsType
{
	public abstract Guid Id { get; }

	public abstract string Name { get; }
}