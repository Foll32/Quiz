using Quiz.Core.Abstractions;

namespace Quiz.QuestionStorage.Db.Models;

public class TextOnlyQuestionFormulation : QuestionFormulation
{
	public string Text { get; set; } = null!;

	public override QuestionFormulationType Type => QuestionFormulationType.TextOnly;
}