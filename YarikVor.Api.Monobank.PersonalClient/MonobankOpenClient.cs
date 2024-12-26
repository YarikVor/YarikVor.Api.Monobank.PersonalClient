using System.Text.Json;
using YarikVor.Api.Monobank.PersonalClient.Abstractions;
using YarikVor.Api.Monobank.PersonalClient.Entities.Dto;
using YarikVor.Api.Monobank.PersonalClient.Entities.Enums;
using YarikVor.Api.Monobank.PersonalClient.Entities.Options;

namespace YarikVor.Api.Monobank.PersonalClient;

public sealed class MonobankClient : IDisposable, IMonobankClient
{
    public const string XToken = "X-Token";
    private readonly HttpClient _httpClient;
    private bool _disposed;

    public MonobankClient(MonobankClientOptions options, HttpClient? httpClient = null)
    {
        _disposed = httpClient != null;
        _httpClient = httpClient ?? new HttpClient();
        _httpClient.DefaultRequestHeaders.Add(XToken, options.Token);
        _httpClient.BaseAddress = options.Url;
    }

    public void Dispose()
    {
        if (_disposed)
            return;
        _disposed = true;
        _httpClient.Dispose();
    }

    public async Task<MonobankResponse<MonoClientInfo>> GetPersonalInfoAsync(CancellationToken ct = default)
    {
        const string url = "personal/client-info";
        return await GetAsync<MonoClientInfo>(url, ct).ConfigureAwait(false);
    }

    public async Task<MonobankResponse<Transaction[]>> GetTransactionsAsync(TransactionRequest request,
        CancellationToken ct = default)
    {
        Validate(request);

        var url =
            $"personal/statement/{request.Account}/" +
            $"{request.From.ToUnixTimeSeconds()}/" +
            $"{request.To?.ToUnixTimeSeconds().ToString() ?? ""}";
        return await GetAsync<Transaction[]>(url, ct).ConfigureAwait(false);
    }

    public async Task<MonobankResponse<CurrencyRate[]>> GetCurrencyRatesAsync(CancellationToken ct = default)
    {
        const string url = "bank/currency";
        return await GetAsync<CurrencyRate[]>(url, ct);
    }

    private async Task<MonobankResponse<T>> GetAsync<T>(string url, CancellationToken ct = default)
    {
        var response = await _httpClient.GetAsync(url, ct).ConfigureAwait(false);

        await using var responseStream = await response.Content.ReadAsStreamAsync(ct).ConfigureAwait(false);

        if (!response.IsSuccessStatusCode)
        {
            var responseType = await GetResponseTypeAsync(responseStream, ct).ConfigureAwait(false);
            return new MonobankResponse<T>
            {
                Type = responseType
            };
        }

        var info = await JsonSerializer.DeserializeAsync<T>(responseStream,
            MonobankUtility.DefaultJsonSerializerOptions, ct);

        if (info is null)
            return new MonobankResponse<T>
            {
                Type = MonobankResponseType.Empty
            };

        return new MonobankResponse<T>
        {
            Type = MonobankResponseType.Success,
            Data = info
        };
    }

    private async ValueTask<MonobankResponseType> GetResponseTypeAsync(Stream stream, CancellationToken ct = default)
    {
        try
        {
            var error = await JsonSerializer.DeserializeAsync<MonoErrorResult>(stream, cancellationToken: ct)
                .ConfigureAwait(false);

            if (error?.ErrorDescription is { } errorDescription)
                return errorDescription.ToLowerInvariant() switch
                {
                    var desc when desc.Contains("requests") => MonobankResponseType.ManyRequests,
                    var desc when desc.Contains("missing") => MonobankResponseType.NotAuthorized,
                    var desc when desc.Contains("unknown") => MonobankResponseType.NotValidToken,
                    _ => MonobankResponseType.StrangerRequest
                };

            return MonobankResponseType.StrangerRequest;
        }
        catch (JsonException)
        {
            return MonobankResponseType.NotFound;
        }
    }

    private static void Validate(TransactionRequest request)
    {
        var toTime = request.To ?? DateTime.UtcNow;
        var delta = toTime - request.From;
        ArgumentOutOfRangeException.ThrowIfGreaterThan(request.From, toTime);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(delta, TimeSpan.FromDays(31));
    }
}