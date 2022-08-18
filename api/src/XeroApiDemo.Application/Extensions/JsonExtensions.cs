using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace XeroApiDemo.Application.Extensions;

public static class JsonExtensions
{
    public static T Deserialize<T>(this string json, JsonCasing jsonCasing = JsonCasing.Default)
    {
        JsonSerializerSettings settings = jsonCasing switch
        {
            JsonCasing.Default => null,
            JsonCasing.Snake => SnakeCasedSettings,
            _ => throw new ArgumentOutOfRangeException(nameof(jsonCasing), jsonCasing, null)
        };
        
        try
        {
            return JsonConvert.DeserializeObject<T>(json, settings);
        }
        catch (Exception e)
        {
            throw new InvalidOperationException($"Failed to deserialize {typeof(T).Name} from {json}", e);
        }
    }
    
    private static JsonSerializerSettings SnakeCasedSettings => new()
    {
        ContractResolver = new DefaultContractResolver
        {
            NamingStrategy = new SnakeCaseNamingStrategy()
        }
    };
}

public enum JsonCasing
{
    Default,
    Snake
}
