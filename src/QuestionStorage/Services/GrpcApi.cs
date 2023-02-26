using AutoMapper;
using Google.Protobuf.Collections;
using Grpc.Core;
using Quiz.Core.Abstractions;
using Quiz.QuestionStorage.Grpc;

namespace Quiz.QuestionStorage.Services;

public class GrpcApi : Grpc.QuestionStorage.QuestionStorageBase
{
	private readonly IQuestionService _questionService;
	private readonly IMapper _mapper;

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
			_ => new QuestionResponse {Error = new Error {Code = (int) ErrorCodes.NotFound}});
	}

	public override async Task<TextOnlyQuestionFormulationResponse> GetTextOnlyQuestionFormulation(QuestionId request, ServerCallContext context)
	{
		if (!Guid.TryParse(request.Value, out var guid))
			return new TextOnlyQuestionFormulationResponse {Error = new Error {Code = (int) ErrorCodes.ValidationError}};

		var result = await _questionService.GetFormulationAsync<Db.Models.TextOnlyQuestionFormulation>(QuestionFormulationType.TextOnly, guid, default);

		return result.Match(
			f => new TextOnlyQuestionFormulationResponse {Formulation = _mapper.Map<TextOnlyQuestionFormulation>(f)},
			//f => new TextOnlyQuestionFormulationResponse {Formulation = new TextOnlyQuestionFormulation{Text = f.Text, NotesForHost = f.NotesForHost ?? string.Empty}},
			_ => new TextOnlyQuestionFormulationResponse {Error = new Error {Code = (int) ErrorCodes.NotFound}},
			_ => new TextOnlyQuestionFormulationResponse {Error = new Error {Code = (int) ErrorCodes.ValidationError}});
	}

	public override async Task<FreeTextAnswerDefinitionResponse> GetFreeTextAnswerDefinition(QuestionId request, ServerCallContext context)
	{
		if (!Guid.TryParse(request.Value, out var guid))
			return new FreeTextAnswerDefinitionResponse {Error = new Error {Code = (int) ErrorCodes.ValidationError}};

		var result = await _questionService.GetAnswerAsync<Db.Models.FreeTextAnswerDefinition>(AnswerDefinitionType.FreeText, guid, default);
		
		return result.Match(
			d => new FreeTextAnswerDefinitionResponse {Definition = _mapper.Map<FreeTextAnswerDefinition>(d)},
			_ => new FreeTextAnswerDefinitionResponse {Error = new Error {Code = (int) ErrorCodes.NotFound}},
			_ => new FreeTextAnswerDefinitionResponse {Error = new Error {Code = (int) ErrorCodes.ValidationError}});
	}
}