using System.Text.Json.Serialization;
using Quiz.Core.Abstractions;

namespace Quiz.QuestionStorage.DTO;

[JsonDerivedType(typeof(TextOnlyQuestionFormulation))]
public class QuestionFormulation
{
	public int Type { get; init; }
	
	public FormattedString? NotesForHost { get; init; }
}