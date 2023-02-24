using Quiz.Core.Abstractions;

namespace Quiz.QuestionStorage.Db.Models;

public sealed class Question
{
	public Guid Id { get; init; }
	
	public QuestionFormulationType QuestionFormulationType { get;init; }

	public AnswerDefinitionType AnswerDefinitionType { get;init; }
}