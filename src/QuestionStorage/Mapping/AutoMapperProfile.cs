using AutoMapper;
using Quiz.QuestionStorage.Contracts;
using TextOnlyQuestionFormulation = Quiz.QuestionStorage.Db.Models.TextOnlyQuestionFormulation;

namespace Quiz.QuestionStorage;

public class AutoMapperProfile : Profile
{
	private const char AdditionalAnswersSeparatorSymbol = '\uFFFF';
	
	public AutoMapperProfile()
	{
		AllowNullCollections = true;
		CreateMap<Db.Models.Question, Question>();
		CreateMap<TextOnlyQuestionFormulation, Contracts.TextOnlyQuestionFormulation>();
		CreateMap<Db.Models.FreeTextAnswerDefinition, FreeTextAnswerDefinition>()
			.ForMember(d => d.AdditionalAnswers, opt => opt.MapFrom((d, _) => d.AdditionalAnswers?.Split(AdditionalAnswersSeparatorSymbol, StringSplitOptions.RemoveEmptyEntries)));
	}
}