using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;
using Quiz.QuestionStorage.Db;
using Quiz.QuestionStorage.Db.Models;

namespace Quiz.QuestionStorage.Services;

internal class QuestionService : IQuestionService
{
	private readonly Context _context;

	public QuestionService(Context context)
	{
		_context = context;
	}

	public async Task<OneOf<Question, NotFound>> GetQuestionAsync(Guid id)
	{
		var question = await _context.Questions.FirstOrDefaultAsync(q => q.Id == id);
		return question is null
			? new NotFound()
			: question;
	}

	public async Task<OneOf<T, NotFound>> GetFormulationAsync<T>(Guid questionId, CancellationToken cancellationToken)
		where T : QuestionFormulation
	{
		IQueryable<QuestionFormulation> dbSet = _context.Set<T>();

		var formulation = await dbSet.FirstOrDefaultAsync(f => f.QuestionId == questionId, cancellationToken);
		if (formulation is T typedFormulation)
			return typedFormulation;

		return new NotFound();
		
	}

	public async Task<OneOf<T, NotFound>> GetAnswerAsync<T>(Guid questionId, CancellationToken cancellationToken)
		where T : AnswerDefinition
	{
		IQueryable<AnswerDefinition> dbSet = _context.Set<T>();

		var answerDefinition = await dbSet.FirstOrDefaultAsync(f => f.QuestionId == questionId, cancellationToken);
		if (answerDefinition is T typedAnswerDefinition)
			return typedAnswerDefinition;

		return new NotFound();
	}
}