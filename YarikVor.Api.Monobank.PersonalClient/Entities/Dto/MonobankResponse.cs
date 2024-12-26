using YarikVor.Api.Monobank.PersonalClient.Entities.Enums;
using YarikVor.Api.Monobank.PersonalClient.Exceptions;

namespace YarikVor.Api.Monobank.PersonalClient.Entities.Dto;

public class MonobankResponse
{
    public MonobankResponseType Type { get; set; }

    public bool IsSuccess => Type is MonobankResponseType.Success;

    public void ThrowIfError()
    {
        if (Type is MonobankResponseType.Success)
            return;

        throw new MonobankApiException(Type);
    }
}

public class MonobankResponse<T> : MonobankResponse
{
    public T? Data { get; set; }
}