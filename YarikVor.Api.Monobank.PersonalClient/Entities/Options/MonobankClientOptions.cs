namespace YarikVor.Api.Monobank.PersonalClient.Entities.Options;

public class MonobankClientOptions
{
    public const string DefaultUrl = "https://api.monobank.ua/";
    public static readonly Uri DefaultUri = new(DefaultUrl);

    public required string Token;
    public Uri Url = DefaultUri;
}