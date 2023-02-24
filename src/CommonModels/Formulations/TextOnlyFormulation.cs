using Quiz.Core.Abstractions;
using Quiz.Core.Abstractions.Formulations;

namespace Quiz.CommonModels.Formulations;

public class TextOnlyFormulation : QuestionFormulation, ITextOnlyFormulation
{
	public const FormulationType FixedFormulationType = FormulationType.TextOnly;

	public TextOnlyFormulation(FormattedString text)
	{
		Type = FixedFormulationType;
		Text = text;
	}
	
	public FormattedString Text { get; }
}