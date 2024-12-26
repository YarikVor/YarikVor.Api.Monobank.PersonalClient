namespace YarikVor.Api.Monobank.PersonalClient.Entities.Dto;

public class MonoClientInfo
{
    public string ClientId { get; set; }
    public string Name { get; set; }
    public string WebHookUrl { get; set; }
    public string Permissions { get; set; }
    public List<MonoAccountInfo> Accounts { get; set; }
    public List<MonoJarInfo> Jars { get; set; }
}