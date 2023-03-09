using Quiz.Core.Abstractions;

namespace Quiz.QuestionStorage.Db.Models;

public class FreeTextAnswerDefinition : AnswerDefinition
{
	public override AnswerDefinitionType Type => AnswerDefinitionType.FreeText;
	
	public string? CorrectAnswer { get; set; }

	public string? AdditionalAnswers { get; set; }

	public override void ClearAnswer()
	{
		base.ClearAnswer();
		
		CorrectAnswer = null;
		AdditionalAnswers = null;
	}
}