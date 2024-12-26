namespace YarikVor.Api.Monobank.PersonalClient.Entities.Dto;

public class CurrencyRate
{
    public int CurrencyCodeA { get; set; }
    public int CurrencyCodeB { get; set; }
    public int Date { get; set; }
    public decimal? RateSell { get; set; }
    public decimal? RateBuy { get; set; }
    public decimal? RateCross { get; set; }
}