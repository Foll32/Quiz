using AutoMapper;
using Quiz.QuestionStorage.DTO;


namespace Quiz.QuestionStorage.Mapper;

public class AutoMapperProfile : Profile
{
	public AutoMapperProfile()
	{
		CreateMap<Db.Models.Question, Question>()
			.ForMember(q => q.Formulation, e => e.MapFrom(q => q.QuestionFormulation))
			.ForMember(q => q.Answer, e => e.MapFrom(q => q.AnswerDefinition));
		CreateMap<Db.Models.Formulations.QuestionFormulation, QuestionFormulation>()
			.IncludeAllDerived();
		CreateMap<Db.Models.Answers.AnswerDefinition, AnswerDefinition>()
			.IncludeAllDerived();
		CreateMap<Db.Models.Formulations.TextOnlyFormulation, TextOnlyQuestionFormulation>();
		CreateMap<Db.Models.Answers.FreeTextAnswerDefinition, FreeTextAnswerDefinition>();
	}
}