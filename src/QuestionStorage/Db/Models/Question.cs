using Quiz.Core.Abstractions;
using Quiz.QuestionStorage.Db.Models.Answers;
using Quiz.QuestionStorage.Db.Models.Formulations;

namespace Quiz.QuestionStorage.Db.Models;

public sealed class Question
{
	public Guid Id { get; init; }

	public long AnswerDefinitionId { get;init; }
	
	public long QuestionFormulationId { get;init; }

	public QuestionFormulation QuestionFormulation { get; init; } = null!;
	
	public AnswerDefinition AnswerDefinition { get; init; } = null!;
}