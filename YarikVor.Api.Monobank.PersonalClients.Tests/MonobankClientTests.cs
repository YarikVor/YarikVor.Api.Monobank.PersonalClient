using YarikVor.Api.Monobank.PersonalClient;
using YarikVor.Api.Monobank.PersonalClient.Entities.Dto;
using YarikVor.Api.Monobank.PersonalClient.Entities.Options;

namespace YarikVor.Api.Monobank.PersonalClients.Tests;

public class MonobankClientTests
{
    [Fact]
    public async Task GetCurrencyRatesAsync_Get_NotEmptyResult()
    {
        var options = new MonobankClientOptions()
        {
            Token = ""
        };
        using var client = new MonobankClient(options);

        var result = await client.GetCurrencyRatesAsync();
        
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
        Assert.NotEmpty(result.Data);
    }

    [Fact]
    public async Task GetCurrencyRatesAsync_Get_ValidResult()
    {
        var options = new MonobankClientOptions()
        {
            Token = ""
        };
        using var client = new MonobankClient(options);

        var result = await client.GetCurrencyRatesAsync();
        
        Assert.True(result.IsSuccess);
        result.ThrowIfError();
    }

    [Fact]
    public async Task GetPersonalInfoAsync_Get_ValidResult()
    {
        var options = new MonobankClientOptions()
        {
            Token = Constants.Token
        };
        using var client = new MonobankClient(options);
        
        var result = await client.GetPersonalInfoAsync();
        
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
        Assert.NotEmpty(result.Data.Name);
    }

    [Fact]
    public async Task? GetTransactionsAsync_FromLast10Days_ValidResult()
    {
        var options = new MonobankClientOptions()
        {
            Token = Constants.Token
        };
        using var client = new MonobankClient(options);

        var request = TransactionRequest.FromNow(TimeSpan.FromDays(10));

        var result = await client.GetTransactionsAsync(request);
        
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
    }

    [Fact]
    public void TransactionRequest_FromNow_WrongDays_Throw()
    {
        var options = new MonobankClientOptions()
        {
            Token = ""
        };
        using var client = new MonobankClient(options);
        Assert.ThrowsAny<ArgumentOutOfRangeException>(() =>
        {
            var request = TransactionRequest.FromNow(TimeSpan.FromDays(100));
        });
    }
    
    [Fact]
    public async Task GetTransactionsAsync_WrongDays_Throw()
    {
        var options = new MonobankClientOptions()
        {
            Token = ""
        };
        using var client = new MonobankClient(options);
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
        {
            var request = new TransactionRequest()
            {
                From = DateTime.Now.AddDays(-100),
                To = DateTime.Now,
            };
            await client.GetTransactionsAsync(request);
        });
    }
    
}