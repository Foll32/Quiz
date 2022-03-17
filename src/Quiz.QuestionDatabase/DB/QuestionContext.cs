using Microsoft.EntityFrameworkCore;
using Quiz.QuestionDatabase.DB.Model;

namespace Quiz.QuestionDatabase.DB;

public class QuestionContext : DbContext
{
	public DbSet<Question> Questions { get; set; }

	public QuestionContext(DbContextOptions options) :
		base(options)
	{
	}
	
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		var entity = modelBuilder.Entity<Question>();
		entity.Property(q => q.Text).HasMaxLength(300);
		entity.Property(q => q.Answer).HasMaxLength(50);
	}
}