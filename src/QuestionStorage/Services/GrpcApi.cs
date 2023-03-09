using AutoMapper;
using FluentValidation;
using Grpc.Core;
using Quiz.QuestionStorage.Grpc;
using Quiz.QuestionStorage.Validators;
using FreeTextAnswerDefinition = Quiz.QuestionStorage.Db.Models.FreeTextAnswerDefinition;
using OneTextChoiceAnswerDefinition = Quiz.QuestionStorage.Db.Models.OneTextChoiceAnswerDefinition;
using TextOnlyQuestionFormulation = Quiz.QuestionStorage.Db.Models.TextOnlyQuestionFormulation;

namespace Quiz.QuestionStorage.Services;

public class GrpcApi : Grpc.QuestionStorage.QuestionStorageBase
{
	private static readonly IValidator<GetQuestionsRequest> GetQuestionsRequestValidator = new GetQuestionsRequestValidator();
	
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

	public override async Task<GetQuestionsResponse> GetQuestions(GetQuestionsRequest request, ServerCallContext context)
	{
		if (!(await GetQuestionsRequestValidator.ValidateAsync(request)).IsValid)
		{
			return new GetQuestionsResponse {Error = _mapper.Map<ErrorCodes, Error>(ErrorCodes.ValidationError)};
		}

		var ids = new List<Guid>(request.Id.Count);
		foreach (var id in request.Id)
		{
			if (!Guid.TryParse(id.Value, out var guid))
				return new GetQuestionsResponse {Error = _mapper.Map<ErrorCodes, Error>(ErrorCodes.ValidationError)};
			
			ids.Add(guid);
		}

		var result = await _questionService.GetQuestionsAsync(ids, context.CancellationToken);

		return _mapper.Map<GetQuestionsResponse>(result);
	}

	public override async Task<TextOnlyQuestionFormulationResponse> GetTextOnlyQuestionFormulation(QuestionFormulationRequest request, ServerCallContext context)
	{
		if (!Guid.TryParse(request.Id.Value, out var guid))
			return new TextOnlyQuestionFormulationResponse {Error = _mapper.Map<ErrorCodes, Error>(ErrorCodes.ValidationError)};

		var result = await _questionService.GetFormulationAsync<TextOnlyQuestionFormulation>(guid, context.CancellationToken);

		return result.Match(
			f => new TextOnlyQuestionFormulationResponse {Formulation = _mapper.Map<Grpc.TextOnlyQuestionFormulation>(f)},
			_ => new TextOnlyQuestionFormulationResponse {Error = _mapper.Map<ErrorCodes, Error>(ErrorCodes.NotFound)});
	}

	public override async Task<FreeTextAnswerDefinitionResponse> GetFreeTextAnswerDefinition(AnswerDefinitionRequest request, ServerCallContext context)
	{
		if (!Guid.TryParse(request.Id.Value, out var guid))
			return new FreeTextAnswerDefinitionResponse {Error = _mapper.Map<ErrorCodes, Error>(ErrorCodes.ValidationError)};

		var result = await _questionService.GetAnswerAsync<FreeTextAnswerDefinition>(guid, request.WithCorrectAnswer, context.CancellationToken);

		return result.Match(
			d => new FreeTextAnswerDefinitionResponse {Definition = _mapper.Map<Grpc.FreeTextAnswerDefinition>(d)},
			_ => new FreeTextAnswerDefinitionResponse {Error = _mapper.Map<ErrorCodes, Error>(ErrorCodes.NotFound)});
	}

	public override async Task<OneTextChoiceAnswerDefinitionResponse> GetOneTextChoiceAnswerDefinition(AnswerDefinitionRequest request, ServerCallContext context)
	{
		if (!Guid.TryParse(request.Id.Value, out var guid))
			return new OneTextChoiceAnswerDefinitionResponse {Error = _mapper.Map<ErrorCodes, Error>(ErrorCodes.ValidationError)};

		var result = await _questionService.GetAnswerAsync<OneTextChoiceAnswerDefinition>(guid, request.WithCorrectAnswer, context.CancellationToken);

		return result.Match(
			d => new OneTextChoiceAnswerDefinitionResponse {Definition = _mapper.Map<Grpc.OneTextChoiceAnswerDefinition>(d)},
			_ => new OneTextChoiceAnswerDefinitionResponse {Error = _mapper.Map<ErrorCodes, Error>(ErrorCodes.NotFound)});
	}

	public override async Task<AddNewQuestionResponse> AddQuestion(NewQuestionRequest request, ServerCallContext context)
	{
		var result = await _questionService.AddQuestion(request, context.CancellationToken);

		return result.Match(
			guid => new AddNewQuestionResponse {Id = new QuestionId {Value = guid.ToString()}},
			_ => new AddNewQuestionResponse {Error = _mapper.Map<ErrorCodes, Error>(ErrorCodes.ValidationError)}
		);
	}
}