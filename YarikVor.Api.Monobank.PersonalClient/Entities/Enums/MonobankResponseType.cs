namespace YarikVor.Api.Monobank.PersonalClient.Entities.Enums;

public enum MonobankResponseType
{
    Empty,
    Success,
    NotFound,
    NotAuthorized,
    NotValidToken,
    ManyRequests,
    StrangerRequest
}