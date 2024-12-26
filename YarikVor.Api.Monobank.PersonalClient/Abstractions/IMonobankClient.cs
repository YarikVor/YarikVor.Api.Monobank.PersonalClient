using YarikVor.Api.Monobank.PersonalClient.Entities.Dto;

namespace YarikVor.Api.Monobank.PersonalClient.Abstractions;

public interface IMonobankClient
{
    Task<MonobankResponse<MonoClientInfo>> GetPersonalInfoAsync(CancellationToken ct = default);

    Task<MonobankResponse<Transaction[]>> GetTransactionsAsync(TransactionRequest request,
        CancellationToken ct = default);

    Task<MonobankResponse<CurrencyRate[]>> GetCurrencyRatesAsync(CancellationToken ct = default);
}