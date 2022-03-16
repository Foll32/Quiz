using Core.Question;

namespace Quiz.QuestionDatabase.Services;

public interface IQuestionRepository
{
	public IQuestion GetQuestion(Guid id);
}