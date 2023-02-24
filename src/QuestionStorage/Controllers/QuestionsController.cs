using Microsoft.AspNetCore.Mvc;
using Quiz.CommonModels;

namespace Quiz.QuestionStorage.Controllers;

[ApiController]
[Route("/[controller]")]
public class QuestionsController : ControllerBase
{

	[HttpGet("{id:guid}")]
	public async Task<Question> GetQuestionAsync([FromRoute]Guid id)
	{
		
		return new Question
		{
			Id = Guid.NewGuid()
		};
	}
}