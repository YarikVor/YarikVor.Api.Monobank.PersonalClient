namespace YarikVor.Api.Monobank.PersonalClient.Entities.Dto;

public class TransactionRequest
{
    public const string DefaultAccount = "0";

    public string Account { get; set; }
    public DateTimeOffset From { get; set; }
    public DateTimeOffset? To { get; set; }

    public static TransactionRequest FromDefaultAccount(DateTimeOffset from, DateTimeOffset? to = null)
    {
        return new TransactionRequest
        {
            Account = DefaultAccount,
            From = from,
            To = to
        };
    }

    public static TransactionRequest FromNow(TimeSpan duration, string account = DefaultAccount)
    {
        ArgumentOutOfRangeException.ThrowIfGreaterThan(duration, TimeSpan.FromDays(31));


        var from = DateTimeOffset.UtcNow.Subtract(duration);
        return new TransactionRequest
        {
            Account = account,
            From = from
        };
    }
}