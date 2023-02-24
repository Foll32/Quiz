using Quiz.Core.Abstractions;
using Quiz.Core.Abstractions.Formulations;

namespace Quiz.QuestionStorage.Db.Models.Formulations;

public class TextOnlyFormulation : QuestionFormulation, ITextOnlyFormulation
{
	public FormattedString Text { get; init; }
}