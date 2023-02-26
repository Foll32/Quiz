using Quiz.Core.Abstractions;

namespace Quiz.QuestionStorage.Db.Models;

public class OneTextChoiceAnswerDefinition : AnswerDefinition
{
	public override AnswerDefinitionType Type => AnswerDefinitionType.OneTextChoice;
	
	/// <summary>
	/// Варианты правильных ответов, объединенных в одну строку.
	/// </summary>
	public string Variants { get; set; } = null!;

	/// <summary>
	/// Индекс правильного варианта.
	/// </summary>
	public byte? CorrectVariant { get; set; }

	public override void ClearAnswer()
	{
		base.ClearAnswer();
		
		CorrectVariant = null;
	}
}