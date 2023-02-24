using Quiz.Core.Abstractions;

namespace Quiz.CommonModels.Formulations;

public abstract class QuestionFormulation : IFormulation
{
	public FormulationType Type { get; init; }
}