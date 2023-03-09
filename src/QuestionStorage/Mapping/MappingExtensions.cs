using Quiz.QuestionStorage.Grpc;

namespace Quiz.QuestionStorage;

internal static class MappingExtensions
{
	public static Error ToError(this ErrorCodes errorCode)
	{
		return new() {Code = (int) errorCode};
	}
}