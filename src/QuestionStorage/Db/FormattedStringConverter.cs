using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Quiz.Core.Abstractions;

namespace Quiz.QuestionStorage.Db;

public class FormattedStringConverter : ValueConverter<FormattedString, string>
{
	[SuppressMessage("Usage", "EF1001:Internal EF Core API usage.")]
	public FormattedStringConverter()
		: base(fs => fs.Text, s => new(s), false)
	{
	}
}