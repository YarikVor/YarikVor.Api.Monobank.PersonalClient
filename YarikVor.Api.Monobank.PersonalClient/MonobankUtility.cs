using System.Text.Json;
using System.Text.Json.Serialization;

namespace YarikVor.Api.Monobank.PersonalClient;

public class MonobankUtility
{
    public static readonly JsonSerializerOptions DefaultJsonSerializerOptions = new(JsonSerializerDefaults.Web)
    {
        PropertyNameCaseInsensitive = true,
        IncludeFields = true,
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
    };
}