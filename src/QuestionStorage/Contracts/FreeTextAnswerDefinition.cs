namespace Quiz.QuestionStorage.Contracts;

public class FreeTextAnswerDefinition : AnswerDefinition
{
	public string CorrectAnswer { get; init; } = null!;
	
	public IReadOnlyCollection<string>? AdditionalAnswers { get; init; }
}