using Newtonsoft.Json;

namespace OrdersWeb.SecurityDemos;

public static class InsecureJson
{
    // VULNERABLE: TypeNameHandling.All can allow attacker-controlled types
    public static object? Deserialize(string payload)
    {
        var settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        };

        return JsonConvert.DeserializeObject(payload, settings);
    }

    // SAFE: no polymorphic type materialization from untrusted input
    public static T? DeserializeSafe<T>(string payload)
        => JsonConvert.DeserializeObject<T>(payload);
}
