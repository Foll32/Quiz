using Microsoft.EntityFrameworkCore;
using Quiz.CommonModels;
using Quiz.CommonModels.Answers;
using Quiz.CommonModels.Formulations;

namespace Quiz.QuestionStorage.Db;

public sealed class Context : DbContext
{
	internal const int TextOnlyFormulationTextMaxLength = 1000;
	internal const int AnswerDefinitionNotesForHostMaxLength = 2000;
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

		var questionFormulationEntity = modelBuilder.Entity<QuestionFormulation>();
		questionFormulationEntity.Property<long>("Id");
		questionFormulationEntity.HasKey("Id");
		questionFormulationEntity.HasDiscriminator(f => f.Type)
			.HasValue<TextOnlyFormulation>(TextOnlyFormulation.FixedFormulationType);
		
		var textOnlyFormulationEntity = modelBuilder.Entity<TextOnlyFormulation>();
		textOnlyFormulationEntity.Property(f => f.Text)
			.HasConversion<FormattedStringConverter>()
			.HasMaxLength(TextOnlyFormulationTextMaxLength);
		
		var answerDefinitionEntity = modelBuilder.Entity<AnswerDefinition>();
		answerDefinitionEntity.Property<long>("Id");
		answerDefinitionEntity.HasKey("Id");
		answerDefinitionEntity.HasDiscriminator(d => d.Type)
			.HasValue<FreeTextAnswerDefinition>(FreeTextAnswerDefinition.FixedAnswerType);
		answerDefinitionEntity.Property(d => d.NotesForHost)
			.HasConversion<FormattedStringConverter>()
			.HasMaxLength(AnswerDefinitionNotesForHostMaxLength);
		answerDefinitionEntity.Property(d => d.NotesForPlayers)
			.HasConversion<FormattedStringConverter>()
			.HasMaxLength(AnswerDefinitionNotesForPlayersMaxLength);
		
		var freeTextAnswerDefinitionEntity = modelBuilder.Entity<FreeTextAnswerDefinition>();
		freeTextAnswerDefinitionEntity.Property(d => d.CorrectAnswer)
			.HasConversion<FormattedStringConverter>()
			.HasMaxLength(FreeTextAnswerDefinitionCorrectAnswerTextMaxLength);
		freeTextAnswerDefinitionEntity.Property(d => d.AdditionalAnswers)
			.HasConversion<FormattedStringCollectionConverter>()
			.HasMaxLength(FreeTextAnswerDefinitionAdditionalAnswersTextMaxLength);
	}
}