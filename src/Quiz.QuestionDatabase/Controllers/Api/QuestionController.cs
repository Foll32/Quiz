using Core.Question;
using Microsoft.AspNetCore.Mvc;
using Quiz.QuestionDatabase.Services;

namespace Quiz.QuestionDatabase.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class QuestionController : ControllerBase
{
	private readonly IQuestionRepository _questionRepository;

	public QuestionController(IQuestionRepository questionRepository)
	{
		_questionRepository = questionRepository;
	}

	[HttpGet("{questionId:guid}")]
	public IQuestion GetQuestion(Guid questionId)
	{
		return _questionRepository.GetQuestion(questionId);
	}
}