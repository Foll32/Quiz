using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quiz.QuestionStorage.Db;
using Quiz.QuestionStorage.DTO;

namespace Quiz.QuestionStorage.Controllers;

[ApiController]
[Route("/[controller]")]
public class QuestionsController : ControllerBase
{
	private readonly Context _context;
	private readonly IMapper _mapper;

	public QuestionsController(Context context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	[HttpGet("{id:guid}")]
	public async Task<IActionResult> GetQuestionAsync([FromRoute]Guid id)
	{
		var question = await _context.Questions
			.Include(q => q.AnswerDefinition)
			.Include(q => q.QuestionFormulation)
			.FirstOrDefaultAsync(q => q.Id == id);

		if (question is null)
			return NotFound();

		var result = _mapper.Map<Question>(question);
		
		return Ok(result);
	}
}