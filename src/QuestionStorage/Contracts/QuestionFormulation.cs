using System.Text.Json.Serialization;

namespace Quiz.QuestionStorage.Contracts;

[JsonDerivedType(typeof(TextOnlyQuestionFormulation))]
public class QuestionFormulation
{
	public string? NotesForHost { get; init; }
}