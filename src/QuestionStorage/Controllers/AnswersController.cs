using Microsoft.AspNetCore.Mvc;
using Quiz.QuestionStorage.Services;

namespace Quiz.QuestionStorage.Controllers;

[ApiController]
[Route("/[controller]")]
public class AnswersController : ControllerBase
{
	private readonly IQuestionService _questionService;

	public AnswersController(IQuestionService questionService)
	{
		_questionService = questionService;
	}

	[HttpGet("{type:int}/{questionId:guid}")]
	public async Task<IActionResult> GetAnswerAsync([FromRoute]int type, [FromRoute]Guid questionId, CancellationToken cancellationToken)
	{
		var result = await _questionService.GetAnswerAsync(type, questionId, cancellationToken);

		return result.Match<IActionResult>(Ok, _ => NotFound(), valError => BadRequest(valError.MapToResponse()));
	}
}