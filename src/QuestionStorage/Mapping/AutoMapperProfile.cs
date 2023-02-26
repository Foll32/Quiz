using AutoMapper;
using Quiz.QuestionStorage.Grpc;
using AnswerDefinitionType = Quiz.Core.Abstractions.AnswerDefinitionType;
using FreeTextAnswerDefinition = Quiz.QuestionStorage.Db.Models.FreeTextAnswerDefinition;
using OneTextChoiceAnswerDefinition = Quiz.QuestionStorage.Db.Models.OneTextChoiceAnswerDefinition;
using Question = Quiz.QuestionStorage.Db.Models.Question;
using QuestionFormulationType = Quiz.Core.Abstractions.QuestionFormulationType;
using TextOnlyQuestionFormulation = Quiz.QuestionStorage.Db.Models.TextOnlyQuestionFormulation;

namespace Quiz.QuestionStorage;

public class AutoMapperProfile : Profile
{
	private const char StringSeparatorSymbol = '\uFFFF';

	public AutoMapperProfile()
	{
		CreateMap<Guid, QuestionId>()
			.ConvertUsing(guid => new QuestionId {Value = guid.ToString()});
		CreateMap<QuestionFormulationType, Grpc.QuestionFormulationType>()
			.ConvertUsing(t => (Grpc.QuestionFormulationType)(int)t);
		CreateMap<AnswerDefinitionType, Grpc.AnswerDefinitionType>()
			.ConvertUsing(t => (Grpc.AnswerDefinitionType)(int)t);
		CreateMap<Question, Grpc.Question>()
			.ForMember(q => q.AnswerType, opt => opt.MapFrom(q => (int) q.AnswerDefinitionType))
			.ForMember(q => q.FormulationType, opt => opt.MapFrom(q => (int) q.QuestionFormulationType));
		CreateMap<TextOnlyQuestionFormulation, Grpc.TextOnlyQuestionFormulation>()
			.ConvertUsing((from, _) =>
			{
				var result = new Grpc.TextOnlyQuestionFormulation {Text = from.Text};

				if (!string.IsNullOrWhiteSpace(from.NotesForHost))
					result.NotesForHost = from.NotesForHost;

				return result;
			});
		CreateMap<FreeTextAnswerDefinition, Grpc.FreeTextAnswerDefinition>()
			.ConvertUsing((from, _) =>
			{
				var result = new Grpc.FreeTextAnswerDefinition {Answer = from.CorrectAnswer};

				if (!string.IsNullOrWhiteSpace(from.NotesForPlayers))
					result.NotesForPlayer = from.NotesForPlayers;

				if (!string.IsNullOrWhiteSpace(from.AdditionalAnswers))
					result.AdditionalAnswers.AddRange(from.AdditionalAnswers.Split(StringSeparatorSymbol, StringSplitOptions.RemoveEmptyEntries));

				return result;
			});
		CreateMap<OneTextChoiceAnswerDefinition, Grpc.OneTextChoiceAnswerDefinition>()
			.ConvertUsing((from, _) =>
			{
				var result = new Grpc.OneTextChoiceAnswerDefinition {CorrectVariant = from.CorrectVariant};
				
				result.AnswerVariants.AddRange(from.Variants.Split(StringSeparatorSymbol, StringSplitOptions.RemoveEmptyEntries));

				if (!string.IsNullOrWhiteSpace(from.NotesForPlayers))
					result.NotesForPlayer = from.NotesForPlayers;

				return result;
			});
		

		CreateMap<ErrorCodes, Error>()
			.ConvertUsing(code => new Error {Code = (int) code});
	}
}