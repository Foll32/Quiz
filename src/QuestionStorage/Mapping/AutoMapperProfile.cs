using AutoMapper;
using Quiz.QuestionStorage.Grpc;

namespace Quiz.QuestionStorage;

public class AutoMapperProfile : Profile
{
	private const char AdditionalAnswersSeparatorSymbol = '\uFFFF';
	
	public AutoMapperProfile()
	{
		CreateMap<Guid, QuestionId>()
			.ConvertUsing(guid => new QuestionId{Value = guid.ToString()});
		CreateMap<Db.Models.Question, Question>()
			.ForMember(q => q.AnswerType, opt => opt.MapFrom(q => (int) q.AnswerDefinitionType))
			.ForMember(q => q.FormulationType, opt => opt.MapFrom(q => (int) q.QuestionFormulationType));
		CreateMap<Db.Models.TextOnlyQuestionFormulation, TextOnlyQuestionFormulation>()
			.ConvertUsing((from, _) =>
			{
				var result = new TextOnlyQuestionFormulation {Text = from.Text};

				if (!string.IsNullOrWhiteSpace(from.NotesForHost))
					result.NotesForHost = from.NotesForHost;

				return result;
			});
		CreateMap<Db.Models.FreeTextAnswerDefinition, FreeTextAnswerDefinition>()
			.ConvertUsing((from, _) =>
			{
				var result = new FreeTextAnswerDefinition {Answer = from.CorrectAnswer};
				
				if (!string.IsNullOrWhiteSpace(from.NotesForPlayers))
					result.NotesForPlayer = from.NotesForPlayers;
				
				if (!string.IsNullOrWhiteSpace(from.AdditionalAnswers))
					result.AdditionalAnswers.AddRange(from.AdditionalAnswers.Split(AdditionalAnswersSeparatorSymbol, StringSplitOptions.RemoveEmptyEntries));
				
				return result;
			});

		CreateMap<ErrorCodes, Error>()
			.ConvertUsing(code => new Error {Code = (int) code});
	}
}