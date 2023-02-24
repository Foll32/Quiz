using Quiz.QuestionStorage.Db.Models.Answers;

namespace Quiz.QuestionStorage.DTO;

public class Question
{
	public Guid Id { get; init; }
	public QuestionFormulation Formulation { get; init; }
	public AnswerDefinition Answer { get; init; }
}