using System.Text.Json.Serialization;

namespace Quiz.QuestionStorage.Contracts;

[JsonDerivedType(typeof(FreeTextAnswerDefinition))]
public class AnswerDefinition
{
	public string? NotesForPlayers { get; init; }
}