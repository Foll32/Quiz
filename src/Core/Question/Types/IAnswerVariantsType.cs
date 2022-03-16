namespace Core.Question.Types;

/// <summary>
/// Тип вариантов ответа.
/// </summary>
public interface IAnswerVariantsType
{
	Guid Id { get; }

	string Name { get; }
}