using AutoMapper;
using Grpc.Core;
using Quiz.Core.Abstractions;
using Quiz.QuestionStorage.Grpc;
using FreeTextAnswerDefinition = Quiz.QuestionStorage.Db.Models.FreeTextAnswerDefinition;
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

		var result = await _questionService.GetQuestionAsync(guid);

		return result.Match(
			q => new QuestionResponse {Question = _mapper.Map<Question>(q)},
			_ => new QuestionResponse {Error = _mapper.Map<ErrorCodes, Error>(ErrorCodes.NotFound)});
	}

	public override async Task<TextOnlyQuestionFormulationResponse> GetTextOnlyQuestionFormulation(QuestionId request, ServerCallContext context)
	{
		if (!Guid.TryParse(request.Value, out var guid))
			return new TextOnlyQuestionFormulationResponse {Error = new Error {Code = (int) ErrorCodes.ValidationError}};

		var result = await _questionService.GetFormulationAsync<TextOnlyQuestionFormulation>(QuestionFormulationType.TextOnly, guid, default);

		return result.Match(
			f => new TextOnlyQuestionFormulationResponse {Formulation = _mapper.Map<Grpc.TextOnlyQuestionFormulation>(f)},
			_ => new TextOnlyQuestionFormulationResponse {Error = _mapper.Map<ErrorCodes, Error>(ErrorCodes.NotFound)},
			_ => new TextOnlyQuestionFormulationResponse {Error = _mapper.Map<ErrorCodes, Error>(ErrorCodes.ValidationError)});
	}

	public override async Task<FreeTextAnswerDefinitionResponse> GetFreeTextAnswerDefinition(QuestionId request, ServerCallContext context)
	{
		if (!Guid.TryParse(request.Value, out var guid))
			return new FreeTextAnswerDefinitionResponse {Error = new Error {Code = (int) ErrorCodes.ValidationError}};

		var result = await _questionService.GetAnswerAsync<FreeTextAnswerDefinition>(AnswerDefinitionType.FreeText, guid, default);

		return result.Match(
			d => new FreeTextAnswerDefinitionResponse {Definition = _mapper.Map<Grpc.FreeTextAnswerDefinition>(d)},
			_ => new FreeTextAnswerDefinitionResponse {Error = _mapper.Map<ErrorCodes, Error>(ErrorCodes.NotFound)},
			_ => new FreeTextAnswerDefinitionResponse {Error = _mapper.Map<ErrorCodes, Error>(ErrorCodes.ValidationError)});
	}
}