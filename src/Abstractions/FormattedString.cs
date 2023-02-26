namespace Quiz.Core.Abstractions;

/// <summary>
/// Строка с форматированием.
/// </summary>
/// <param name="Text">Исходный текст с символами форматирования.</param>
public record struct FormattedString(string Text)
{
	public static explicit operator FormattedString(string text)
	{
		return new(text);
	}
}