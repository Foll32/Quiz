namespace Quiz.QuestionStorage.Db.Models;

public class OneTextChoiceAnswerDefinition : AnswerDefinition
{
	/// <summary>
	/// Варианты правильных ответов, объединенных в одну строку.
	/// </summary>
	public string Variants { get; init; } = null!;

	/// <summary>
	/// Индекс правильного варианта.
	/// </summary>
	public byte CorrectVariant { get; init; }
}