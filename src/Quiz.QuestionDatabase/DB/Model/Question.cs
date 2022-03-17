using Core.Question;
using Core.Question.Statement;
using Core.Question.Types;

namespace Quiz.QuestionDatabase.DB.Model;

public class Question :
	IQuestion
{
	public Guid Id { get; set; }

	public string Text { get; set; }

	public string Answer { get; set; }

	IStatement IQuestion.Statement => new TextStatement(Text);
	
	IAnswerVariants IQuestion.AnswerVariants { get; }

	public Question(string text, string answer)
	{
		Text = text;
		Answer = answer;
	}
}