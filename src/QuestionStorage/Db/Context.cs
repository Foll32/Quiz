using Microsoft.EntityFrameworkCore;
using Quiz.QuestionStorage.Db.Models;

namespace Quiz.QuestionStorage.Db;

public sealed class Context : DbContext
{
	internal const int TextOnlyFormulationTextMaxLength = 1000;
	internal const int QuestionFormulationNotesForHostMaxLength = 2000;
	internal const int AnswerDefinitionNotesForPlayersMaxLength = 2000;
	internal const int FreeTextAnswerDefinitionCorrectAnswerTextMaxLength = 150;
	internal const int FreeTextAnswerDefinitionAdditionalAnswersTextMaxLength = 1500;

	public Context(DbContextOptions<Context> options) : base(options)
	{
	}

	public DbSet<Question> Questions { get; set; } = null!;
	public DbSet<FreeTextAnswerDefinition> FreeTextAnswerDefinitions { get; set; } = null!;
	public DbSet<TextOnlyQuestionFormulation> TextOnlyFormulations { get; set; } = null!;

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		var questionEntity = modelBuilder.Entity<Question>();
		questionEntity.HasKey(q => q.Id);

		var questionFormulationEntity = modelBuilder.Entity<QuestionFormulation>();
		questionFormulationEntity.UseTpcMappingStrategy();
		questionFormulationEntity.HasKey(f => f.QuestionId);
		questionFormulationEntity.Property(f => f.NotesForHost).HasMaxLength(QuestionFormulationNotesForHostMaxLength);

		var textOnlyFormulationEntity = modelBuilder.Entity<TextOnlyQuestionFormulation>();
		textOnlyFormulationEntity.HasBaseType<QuestionFormulation>();
		textOnlyFormulationEntity.Property(f => f.Text).HasMaxLength(TextOnlyFormulationTextMaxLength);

		var answerDefinitionEntity = modelBuilder.Entity<AnswerDefinition>();
		answerDefinitionEntity.UseTpcMappingStrategy();
		answerDefinitionEntity.HasKey(d => d.QuestionId);
		answerDefinitionEntity.Property(d => d.NotesForPlayers)
			.HasMaxLength(AnswerDefinitionNotesForPlayersMaxLength);

		var freeTextAnswerDefinitionEntity = modelBuilder.Entity<FreeTextAnswerDefinition>();
		freeTextAnswerDefinitionEntity.HasBaseType<AnswerDefinition>();
		freeTextAnswerDefinitionEntity.Property(d => d.CorrectAnswer)
			.HasMaxLength(FreeTextAnswerDefinitionCorrectAnswerTextMaxLength);
		freeTextAnswerDefinitionEntity.Property(d => d.AdditionalAnswers)
			.HasMaxLength(FreeTextAnswerDefinitionAdditionalAnswersTextMaxLength);
	}
}