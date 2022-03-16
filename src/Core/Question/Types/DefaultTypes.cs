namespace Core.Question.Types;

public static class DefaultTypes
{
	public static IDictionary<Type, IStatementType> StatementTypes = new Dictionary<Type, IStatementType>
	{
		{typeof(TextStatementType), new TextStatementType()}
	};

	public static IDictionary<Type, IAnswerVariantsType> AnswerVariantsTypes = new Dictionary<Type, IAnswerVariantsType>
	{
		{typeof(TextInputAnswerVariantsType), new TextInputAnswerVariantsType()},
		{typeof(SingleAnswerVariantsOptionsType), new SingleAnswerVariantsOptionsType()},
		{typeof(MultipleAnswerVariantsOptionsType), new MultipleAnswerVariantsOptionsType()}
	};
}