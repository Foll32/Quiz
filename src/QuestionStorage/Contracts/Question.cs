using System.Text.Json.Serialization;
using Quiz.Core.Abstractions;

namespace Quiz.QuestionStorage.Contracts;

public class Question
{
	[JsonPropertyName("id")]
	public Guid Id { get; init; }
	
	[JsonPropertyName("formulationType")]
	public QuestionFormulationType QuestionFormulationType { get; init; }
	
	[JsonPropertyName("answerType")]
	public AnswerDefinitionType AnswerDefinitionType { get; init; }
}