using YarikVor.Api.Monobank.PersonalClient.Entities.Enums;

namespace YarikVor.Api.Monobank.PersonalClient.Exceptions;

public class MonobankApiException(MonobankResponseType responseType, Exception? innerException = null)
    : ApplicationException($"MonoApi Error: {responseType}", innerException)
{
    public readonly MonobankResponseType ResponseType = responseType;
}