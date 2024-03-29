﻿using AutoMapper;
using Quiz.QuestionStorage.Db.Models;
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
		CreateMap<Guid, Grpc.QuestionId>()
			.ConvertUsing(guid => new Grpc.QuestionId {Value = guid.ToString()});
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
				var result = new Grpc.FreeTextAnswerDefinition();

				if (string.IsNullOrEmpty(from.CorrectAnswer))
					result.ClearAnswer();
				else
					result.Answer = from.CorrectAnswer;

				if (!string.IsNullOrWhiteSpace(from.NotesForPlayers))
					result.NotesForPlayer = from.NotesForPlayers;

				if (string.IsNullOrEmpty(from.AdditionalAnswers))
					result.ClearAnswer();
				else
					result.AdditionalAnswers.AddRange(from.AdditionalAnswers.Split(StringSeparatorSymbol, StringSplitOptions.RemoveEmptyEntries));


				return result;
			});
		CreateMap<OneTextChoiceAnswerDefinition, Grpc.OneTextChoiceAnswerDefinition>()
			.ConvertUsing((from, _) =>
			{
				var result = new Grpc.OneTextChoiceAnswerDefinition();
				
				if (from.CorrectVariant is null)
					result.ClearCorrectVariant();
				else
					result.CorrectVariant = (int)from.CorrectVariant;
				
				result.AnswerVariants.AddRange(from.Variants.Split(StringSeparatorSymbol, StringSplitOptions.RemoveEmptyEntries));

				if (!string.IsNullOrWhiteSpace(from.NotesForPlayers))
					result.NotesForPlayer = from.NotesForPlayers;

				return result;
			});
		CreateMap<ErrorCodes, Grpc.Error>()
			.ConvertUsing(code => new Grpc.Error {Code = (int) code});
		CreateMap<IEnumerable<Question>, Grpc.GetQuestionsResponse>()
			.ConvertUsing((from, _, context) =>
			{
				var questionList = new Grpc.QuestionList();
				questionList.Questions.AddRange(from.Select(q => context.Mapper.Map<Grpc.Question>(q)));
				return new Grpc.GetQuestionsResponse {Questions = questionList};
			});
		
		

		CreateMap<Grpc.NewQuestionRequest, QuestionFormulation>()
			.ConvertUsing((from, _, context) =>
			{
				switch (from.FormulationCase)
				{
					case Grpc.NewQuestionRequest.FormulationOneofCase.TextOnlyFormulation:
						return context.Mapper.Map<TextOnlyQuestionFormulation>(from.TextOnlyFormulation);
					default:
						throw new NotSupportedException();
				} 
			});
		CreateMap<Grpc.TextOnlyQuestionFormulation, TextOnlyQuestionFormulation>()
			.ConvertUsing((from, _) =>
			{
				var to = new TextOnlyQuestionFormulation {Text = from.Text};
				
				if (from.HasNotesForHost && !string.IsNullOrWhiteSpace(from.NotesForHost))
					to.NotesForHost = from.NotesForHost;
				
				return to;
			});
		
		CreateMap<Grpc.NewQuestionRequest, AnswerDefinition>()
			.ConvertUsing((from, _, context) =>
			{
				switch (from.AnswerCase)
				{
					case Grpc.NewQuestionRequest.AnswerOneofCase.FreeTextAnswer:
						return context.Mapper.Map<FreeTextAnswerDefinition>(from.FreeTextAnswer);
					case Grpc.NewQuestionRequest.AnswerOneofCase.OneTextChoiceAnswer:
						return context.Mapper.Map<OneTextChoiceAnswerDefinition>(from.OneTextChoiceAnswer);
						
					default:
						throw new NotSupportedException();
				} 
			});
		CreateMap<Grpc.FreeTextAnswerDefinition, FreeTextAnswerDefinition>()
			.ConvertUsing((from, _) =>
			{
				var to = new FreeTextAnswerDefinition
				{
					CorrectAnswer = from.Answer,
					AdditionalAnswers = from.AdditionalAnswers.Any() ? string.Join(StringSeparatorSymbol, from.AdditionalAnswers) : null
				};
				
				if (from.HasNotesForPlayer && !string.IsNullOrWhiteSpace(from.NotesForPlayer))
					to.NotesForPlayers = from.NotesForPlayer;

				return to;
			});
		CreateMap<Grpc.OneTextChoiceAnswerDefinition, OneTextChoiceAnswerDefinition>()
			.ConvertUsing((from, _) =>
			{
				var to = new OneTextChoiceAnswerDefinition
				{
					CorrectVariant = (byte)from.CorrectVariant,
					Variants = string.Join(StringSeparatorSymbol, from.AnswerVariants)
				};
				
				if (from.HasNotesForPlayer && !string.IsNullOrWhiteSpace(from.NotesForPlayer))
					to.NotesForPlayers = from.NotesForPlayer;

				return to;
			});



	}
}