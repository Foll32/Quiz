using Microsoft.AspNetCore.Mvc;
using Quiz.QuestionDatabase.DB;
using Quiz.QuestionDatabase.DB.Model;

namespace Quiz.QuestionDatabase.Controllers;

[ApiController]
[Route("[controller]")]
public class QuestionController : ControllerBase
{
	private readonly QuestionContext _context;

	public QuestionController(QuestionContext context)
	{
		_context = context;
	}

	[HttpGet]
	public IActionResult GetQuestion(Guid guid)
	{
		var question = _context.Questions.FirstOrDefault(q => q.Id == guid);
		if (question == null)
			return NotFound();
		return new ObjectResult(question);
	}

	[HttpPost]
	public async Task<Guid> AddQuestion(string statement, string answer)
	{
		var q = new Question(statement, answer);
		_context.Questions.Add(q);
		await _context.SaveChangesAsync();
		return q.Id;
	}
}