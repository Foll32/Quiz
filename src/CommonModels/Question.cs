using Quiz.CommonModels.Answers;
using Quiz.CommonModels.Formulations;
using Quiz.Core.Abstractions;

namespace Quiz.CommonModels;

public sealed class Question : IQuestion
{
	public Guid Id { get; init; }

	public QuestionFormulation QuestionFormulation { get; init; } = null!;

	public AnswerDefinition AnswerDefinition { get; init; } = null!;

	IFormulation IQuestion.QuestionFormulation => QuestionFormulation;

	IAnswerDefinition IQuestion.AnswerDefinition => AnswerDefinition;
}