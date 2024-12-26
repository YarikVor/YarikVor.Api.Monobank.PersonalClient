using YarikVor.Api.Monobank.PersonalClient.Entities.Enums;

namespace YarikVor.Api.Monobank.PersonalClient.Entities.Dto;

public class MonoAccountInfo
{
    public string Id { get; set; }
    public string SendId { get; set; }
    public long Balance { get; set; }
    public long CreditLimit { get; set; }
    public AccountType Type { get; set; }
    public int CurrencyCode { get; set; }
    public CashbackType CashbackType { get; set; }
    public List<string> MaskedPan { get; set; }
    public string Iban { get; set; }
}