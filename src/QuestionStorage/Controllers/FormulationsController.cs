using Microsoft.AspNetCore.Mvc;
using Quiz.QuestionStorage.Services;

namespace Quiz.QuestionStorage.Controllers;

[ApiController]
[Route("/[controller]")]
public class FormulationsController : ControllerBase
{
	private readonly IQuestionService _questionService;

	public FormulationsController(IQuestionService questionService)
	{
		_questionService = questionService;
	}

	[HttpGet("{type:int}/{questionId:guid}")]
	public async Task<IActionResult> GetFormulationAsync([FromRoute]int type, [FromRoute]Guid questionId, CancellationToken cancellationToken)
	{
		var result = await _questionService.GetFormulationAsync(type, questionId, cancellationToken);

		return result.Match<IActionResult>(Ok, _ => NotFound(), valError => BadRequest(valError.MapToResponse()));
	}
}