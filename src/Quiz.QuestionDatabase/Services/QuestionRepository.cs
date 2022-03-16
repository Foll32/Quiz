using Core.Question;

namespace Quiz.QuestionDatabase.Services;

public class QuestionRepository :
	IQuestionRepository
{
	public IQuestion GetQuestion(Guid id)
	{
		throw new NotImplementedException();
	}
}