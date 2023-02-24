using Microsoft.AspNetCore.Mvc;
using Quiz.QuestionStorage.Services;

namespace Quiz.QuestionStorage.Controllers;

[ApiController]
[Route("/[controller]")]
public class QuestionsController : ControllerBase
{
	private readonly IQuestionService _questionService;

	public QuestionsController(IQuestionService questionService)
	{
		_questionService = questionService;
	}

	[HttpGet("{id:guid}")]
	public async Task<IActionResult> GetQuestionAsync([FromRoute]Guid id)
	{
		var result = await _questionService.GetQuestionAsync(id);

		return result.Match<IActionResult>(Ok, _ => NotFound());
	}
}