using System.Text.Json.Serialization;
using Quiz.Core.Abstractions;

namespace Quiz.QuestionStorage.DTO;

[JsonDerivedType(typeof(FreeTextAnswerDefinition))]
public class AnswerDefinition
{
	public int Type { get; init; }
	
	public FormattedString? NotesForPlayers { get; init; }
}