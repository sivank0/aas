#region

using System.Buffers;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

#endregion

namespace AAS.Tools.Converters;

public class TextJsonSerializer : IJsonSerializer
{
    public static JsonSerializerOptions DefaultOptions { get; set; } = Configure(new JsonSerializerOptions());
    public JsonSerializerOptions Options => DefaultOptions;

    public static JsonSerializerOptions Configure(JsonSerializerOptions options)
    {
        options.PropertyNameCaseInsensitive = true;
        options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

        return options;
    }

    public object? Deserialize(ReadOnlySpan<byte> utf8Json,
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors |
                                    DynamicallyAccessedMemberTypes.PublicFields |
                                    DynamicallyAccessedMemberTypes.PublicProperties)]
        Type returnType, JsonSerializerOptions? options = null)
    {
        return JsonSerializer.Deserialize(utf8Json, returnType, options ?? DefaultOptions);
    }

    public object? Deserialize(string json,
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors |
                                    DynamicallyAccessedMemberTypes.PublicFields |
                                    DynamicallyAccessedMemberTypes.PublicProperties)]
        Type returnType, JsonSerializerOptions? options = null)
    {
        return JsonSerializer.Deserialize(json, returnType, options ?? DefaultOptions);
    }

    public object? Deserialize(ref Utf8JsonReader reader,
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors |
                                    DynamicallyAccessedMemberTypes.PublicFields |
                                    DynamicallyAccessedMemberTypes.PublicProperties)]
        Type returnType, JsonSerializerOptions? options = null)
    {
        return JsonSerializer.Deserialize(ref reader, returnType, options ?? DefaultOptions);
    }

    public TValue? Deserialize<TValue>(ReadOnlySpan<byte> utf8Json, JsonSerializerOptions? options = null)
    {
        return JsonSerializer.Deserialize<TValue>(utf8Json, options ?? DefaultOptions);
    }

    public TValue? Deserialize<TValue>(string json, JsonSerializerOptions? options = null)
    {
        return JsonSerializer.Deserialize<TValue>(json, options ?? DefaultOptions);
    }

    public TValue? Deserialize<TValue>(JsonElement element, JsonSerializerOptions? options = null)
    {
        ArrayBufferWriter<byte> bufferWriter = new();
        using Utf8JsonWriter writer = new(bufferWriter);

        element.WriteTo(writer);
        writer.Flush();

        return JsonSerializer.Deserialize<TValue>(bufferWriter.WrittenSpan, options);
    }

    public TValue? Deserialize<TValue>(ref Utf8JsonReader reader, JsonSerializerOptions? options = null)
    {
        return JsonSerializer.Deserialize<TValue>(ref reader, options ?? DefaultOptions);
    }

    public ValueTask<object?> DeserializeAsync(Stream utf8Json,
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors |
                                    DynamicallyAccessedMemberTypes.PublicFields |
                                    DynamicallyAccessedMemberTypes.PublicProperties)]
        Type returnType, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default)
    {
        return JsonSerializer.DeserializeAsync(utf8Json, returnType, options ?? DefaultOptions, cancellationToken);
    }

    public ValueTask<TValue?> DeserializeAsync<TValue>(Stream utf8Json, JsonSerializerOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        return JsonSerializer.DeserializeAsync<TValue>(utf8Json, options ?? DefaultOptions, cancellationToken);
    }

    public string Serialize(object? value,
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields |
                                    DynamicallyAccessedMemberTypes.PublicProperties)]
        Type inputType, JsonSerializerOptions? options = null)
    {
        return JsonSerializer.Serialize(value, inputType, options ?? DefaultOptions);
    }

    public void Serialize(Utf8JsonWriter writer, object? value,
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields |
                                    DynamicallyAccessedMemberTypes.PublicProperties)]
        Type inputType, JsonSerializerOptions? options = null)
    {
        JsonSerializer.Serialize(writer, value, inputType, options ?? DefaultOptions);
    }

    public void Serialize<TValue>(Utf8JsonWriter writer, TValue value, JsonSerializerOptions? options = null)
    {
        JsonSerializer.Serialize(writer, value, options ?? DefaultOptions);
    }

    public string Serialize<TValue>(TValue value, JsonSerializerOptions? options = null)
    {
        return JsonSerializer.Serialize(value, options ?? DefaultOptions);
    }

    public Task SerializeAsync(Stream utf8Json, object? value,
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields |
                                    DynamicallyAccessedMemberTypes.PublicProperties)]
        Type inputType, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default)
    {
        return JsonSerializer.SerializeAsync(utf8Json, value, inputType, options ?? DefaultOptions, cancellationToken);
    }

    public Task SerializeAsync<TValue>(Stream utf8Json, TValue value, JsonSerializerOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        return JsonSerializer.SerializeAsync(utf8Json, value, options ?? DefaultOptions, cancellationToken);
    }

    public byte[] SerializeToUtf8Bytes(object? value,
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields |
                                    DynamicallyAccessedMemberTypes.PublicProperties)]
        Type inputType, JsonSerializerOptions? options = null)
    {
        return JsonSerializer.SerializeToUtf8Bytes(value, inputType, options ?? DefaultOptions);
    }

    public byte[] SerializeToUtf8Bytes<TValue>(TValue value, JsonSerializerOptions? options = null)
    {
        return JsonSerializer.SerializeToUtf8Bytes(value, options ?? DefaultOptions);
    }
}