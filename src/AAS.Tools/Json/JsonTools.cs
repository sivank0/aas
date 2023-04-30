#region

using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using AAS.Tools.Json.Converters;

#endregion

namespace AAS.Tools.Json;

public static class JsonTools
{
    public static JsonSerializerOptions DefaultOptions { get; set; } = AddJsonSettings(new JsonSerializerOptions());

    public static JsonSerializerOptions Options = new();

    private static readonly JsonConverter[] Converters =
    {
        new IDJsonConverter(), new DateOnlyJsonConverter()
    };

    public static JsonSerializerOptions AddJsonSettings(this JsonSerializerOptions options)
    {
        options.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
        options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

        Options = options;
        return options;
    }

    public static JsonSerializerOptions ApplyToolsConverters(this JsonSerializerOptions options)
    {
        foreach (JsonConverter converter in Converters)
            options.Converters.Add(converter);

        Options = options;
        return options;
    }

    public static JsonSerializerOptions ApplyAnyTypeConverters(this JsonSerializerOptions options, Assembly forAssembly)
    {
        options.Converters.Add(new AnyTypeJsonConverterFactory(forAssembly));

        Options = options;
        return options;
    }

    public static string Serialize(this object @object)
    {
        if (@object is null) return null;
        return JsonSerializer.Serialize(@object, Options);
    }

    public static T? Deserialize<T>(this string @string)
    {
        if (@string is null) return default;
        return JsonSerializer.Deserialize<T>(@string, new JsonSerializerOptions
        {
            DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
    }
}