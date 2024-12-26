namespace YarikVor.Api.Monobank.PersonalClient.Entities.Dto;

public class MonoJarInfo
{
    public string Id { get; set; }
    public string SendId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int CurrencyCode { get; set; }
    public long Balance { get; set; }
    public long? Goal { get; set; }
}