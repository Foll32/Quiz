using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Quiz.Core.Abstractions;

namespace Quiz.QuestionStorage.Db;

public class FormattedStringCollectionConverter : ValueConverter<IReadOnlyCollection<FormattedString>?, string?>
{
	internal const char FreeTextAnswerDefinitionAdditionalAnswersSeparator = '\uFFFF';
	
	[SuppressMessage("Usage", "EF1001:Internal EF Core API usage.")]
	public FormattedStringCollectionConverter()
		: base(collection => collection == null ? null : string.Join(FreeTextAnswerDefinitionAdditionalAnswersSeparator, collection.Select(fs => fs.Text)),
			answerString => answerString == null
				? Array.Empty<FormattedString>()
				: answerString.Split(FreeTextAnswerDefinitionAdditionalAnswersSeparator, StringSplitOptions.RemoveEmptyEntries).Select(text => new FormattedString(text))
					.ToArray(), false)
	{
	}
}