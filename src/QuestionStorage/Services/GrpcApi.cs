using AutoMapper;
using Grpc.Core;
using Quiz.QuestionStorage.Grpc;
using FreeTextAnswerDefinition = Quiz.QuestionStorage.Db.Models.FreeTextAnswerDefinition;
using OneTextChoiceAnswerDefinition = Quiz.QuestionStorage.Db.Models.OneTextChoiceAnswerDefinition;
using TextOnlyQuestionFormulation = Quiz.QuestionStorage.Db.Models.TextOnlyQuestionFormulation;

namespace Quiz.QuestionStorage.Services;

public class GrpcApi : Grpc.QuestionStorage.QuestionStorageBase
{
	private readonly IMapper _mapper;
	private readonly IQuestionService _questionService;

	public GrpcApi(IQuestionService questionService, IMapper mapper)
	{
		_questionService = questionService;
		_mapper = mapper;
	}

	public override Task<Empty> Ping(Empty request, ServerCallContext context)
	{
		return Task.FromResult(new Empty());
	}

	public override async Task<QuestionResponse> GetQuestion(QuestionId request, ServerCallContext context)
	{
		if (!Guid.TryParse(request.Value, out var guid))
			return new QuestionResponse {Error = new Error {Code = (int) ErrorCodes.ValidationError}};

		var result = await _questionService.GetQuestionAsync(guid, context.CancellationToken);

		return result.Match(
			q => new QuestionResponse {Question = _mapper.Map<Question>(q)},
			_ => new QuestionResponse {Error = _mapper.Map<ErrorCodes, Error>(ErrorCodes.NotFound)});
	}

	public override async Task<TextOnlyQuestionFormulationResponse> GetTextOnlyQuestionFormulation(QuestionId request, ServerCallContext context)
	{
		if (!Guid.TryParse(request.Value, out var guid))
			return new TextOnlyQuestionFormulationResponse {Error = new Error {Code = (int) ErrorCodes.ValidationError}};

		var result = await _questionService.GetFormulationAsync<TextOnlyQuestionFormulation>(guid, context.CancellationToken);

		return result.Match(
			f => new TextOnlyQuestionFormulationResponse {Formulation = _mapper.Map<Grpc.TextOnlyQuestionFormulation>(f)},
			_ => new TextOnlyQuestionFormulationResponse {Error = _mapper.Map<ErrorCodes, Error>(ErrorCodes.NotFound)});
	}

	public override async Task<FreeTextAnswerDefinitionResponse> GetFreeTextAnswerDefinition(QuestionId request, ServerCallContext context)
	{
		if (!Guid.TryParse(request.Value, out var guid))
			return new FreeTextAnswerDefinitionResponse {Error = new Error {Code = (int) ErrorCodes.ValidationError}};

		var result = await _questionService.GetAnswerAsync<FreeTextAnswerDefinition>( guid, context.CancellationToken);

		return result.Match(
			d => new FreeTextAnswerDefinitionResponse {Definition = _mapper.Map<Grpc.FreeTextAnswerDefinition>(d)},
			_ => new FreeTextAnswerDefinitionResponse {Error = _mapper.Map<ErrorCodes, Error>(ErrorCodes.NotFound)});
	}

	public override async Task<OneTextChoiceAnswerDefinitionResponse> GetOneTextChoiceAnswerDefinition(QuestionId request, ServerCallContext context)
	{
		if (!Guid.TryParse(request.Value, out var guid))
			return new OneTextChoiceAnswerDefinitionResponse {Error = new Error {Code = (int) ErrorCodes.ValidationError}};

		var result = await _questionService.GetAnswerAsync<OneTextChoiceAnswerDefinition>(guid, context.CancellationToken);

		return result.Match(
			d => new OneTextChoiceAnswerDefinitionResponse {Definition = _mapper.Map<Grpc.OneTextChoiceAnswerDefinition>(d)},
			_ => new OneTextChoiceAnswerDefinitionResponse {Error = _mapper.Map<ErrorCodes, Error>(ErrorCodes.NotFound)});
	}

	public override async Task<AddNewQuestionResponse> AddQuestion(NewQuestion request, ServerCallContext context)
	{
		var result = await _questionService.AddQuestion(request, context.CancellationToken);

		return result.Match(
			guid => new AddNewQuestionResponse{Id = new QuestionId{Value = guid.ToString()}},
			_ => new AddNewQuestionResponse {Error = new Error {Code = (int) ErrorCodes.ValidationError}}
			);
	}
}