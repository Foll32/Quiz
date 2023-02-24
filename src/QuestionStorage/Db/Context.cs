using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Quiz.Core.Abstractions;
using Quiz.QuestionStorage.Db.Models;
using Quiz.QuestionStorage.Db.Models.Answers;
using Quiz.QuestionStorage.Db.Models.Formulations;

namespace Quiz.QuestionStorage.Db;

public sealed class Context : DbContext
{
	internal const int TextOnlyFormulationTextMaxLength = 1000;
	internal const int QuestionFormulationNotesForHostMaxLength = 2000;
	internal const int AnswerDefinitionNotesForPlayersMaxLength = 2000;
	internal const int FreeTextAnswerDefinitionCorrectAnswerTextMaxLength = 150;
	internal const int FreeTextAnswerDefinitionAdditionalAnswersTextMaxLength = 1500;

	public DbSet<Question> Questions { get; set; } = null!;

	public Context(DbContextOptions<Context> options) : base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		var questionEntity = modelBuilder.Entity<Question>();
		questionEntity.HasKey(q => q.Id);
		questionEntity.Property(q => q.Id).HasValueGenerator<GuidValueGenerator>();
		questionEntity.HasOne(q => q.AnswerDefinition).WithOne(d => d.Question).HasForeignKey<Question>(q => q.AnswerDefinitionId);
		questionEntity.HasOne(q => q.QuestionFormulation).WithOne(f => f.Question).HasForeignKey<Question>(q => q.QuestionFormulationId);

		var questionFormulationEntity = modelBuilder.Entity<QuestionFormulation>();
		questionFormulationEntity.HasKey(f => f.Id);
		questionFormulationEntity.HasDiscriminator(f => f.Type)
			.HasValue<TextOnlyFormulation>(FormulationType.TextOnly);
		questionFormulationEntity.Property(f => f.NotesForHost)
			.HasConversion<FormattedStringConverter>()
			.HasMaxLength(QuestionFormulationNotesForHostMaxLength);

		var textOnlyFormulationEntity = modelBuilder.Entity<TextOnlyFormulation>();
		textOnlyFormulationEntity.HasBaseType<QuestionFormulation>();
		textOnlyFormulationEntity.Property(f => f.Text)
			.HasConversion<FormattedStringConverter>()
			.HasMaxLength(TextOnlyFormulationTextMaxLength);
		
		var answerDefinitionEntity = modelBuilder.Entity<AnswerDefinition>();
		answerDefinitionEntity.HasKey(d => d.Id);
		answerDefinitionEntity.HasDiscriminator(d => d.Type)
			.HasValue<FreeTextAnswerDefinition>(AnswerType.FreeText);
		answerDefinitionEntity.Property(d => d.NotesForPlayers)
			.HasConversion<FormattedStringConverter>()
			.HasMaxLength(AnswerDefinitionNotesForPlayersMaxLength);
		
		var freeTextAnswerDefinitionEntity = modelBuilder.Entity<FreeTextAnswerDefinition>();
		freeTextAnswerDefinitionEntity.HasBaseType<AnswerDefinition>();
		freeTextAnswerDefinitionEntity.Property(d => d.CorrectAnswer)
			.HasConversion<FormattedStringConverter>()
			.HasMaxLength(FreeTextAnswerDefinitionCorrectAnswerTextMaxLength);
		freeTextAnswerDefinitionEntity.Property(d => d.AdditionalAnswers)
			.HasConversion<FormattedStringCollectionConverter>()
			.HasMaxLength(FreeTextAnswerDefinitionAdditionalAnswersTextMaxLength);
	}
}