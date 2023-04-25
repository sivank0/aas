#region

using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

#endregion

namespace AAS.Tools.Converters;

public interface IJsonSerializer
{
    JsonSerializerOptions Options { get; }

    object? Deserialize(ReadOnlySpan<byte> utf8Json,
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors |
                                    DynamicallyAccessedMemberTypes.PublicFields |
                                    DynamicallyAccessedMemberTypes.PublicProperties)]
        Type returnType, JsonSerializerOptions? options = null);

    object? Deserialize(string json,
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors |
                                    DynamicallyAccessedMemberTypes.PublicFields |
                                    DynamicallyAccessedMemberTypes.PublicProperties)]
        Type returnType, JsonSerializerOptions? options = null);

    object? Deserialize(ref Utf8JsonReader reader,
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors |
                                    DynamicallyAccessedMemberTypes.PublicFields |
                                    DynamicallyAccessedMemberTypes.PublicProperties)]
        Type returnType, JsonSerializerOptions? options = null);

    TValue? Deserialize<TValue>(ReadOnlySpan<byte> utf8Json, JsonSerializerOptions? options = null);
    TValue? Deserialize<TValue>(string json, JsonSerializerOptions? options = null);
    TValue? Deserialize<TValue>(JsonElement element, JsonSerializerOptions? options = null);
    TValue? Deserialize<TValue>(ref Utf8JsonReader reader, JsonSerializerOptions? options = null);

    ValueTask<object?> DeserializeAsync(Stream utf8Json,
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors |
                                    DynamicallyAccessedMemberTypes.PublicFields |
                                    DynamicallyAccessedMemberTypes.PublicProperties)]
        Type returnType, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default);

    ValueTask<TValue?> DeserializeAsync<TValue>(Stream utf8Json, JsonSerializerOptions? options = null,
        CancellationToken cancellationToken = default);

    string Serialize(object? value,
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields |
                                    DynamicallyAccessedMemberTypes.PublicProperties)]
        Type inputType, JsonSerializerOptions? options = null);

    void Serialize(Utf8JsonWriter writer, object? value,
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields |
                                    DynamicallyAccessedMemberTypes.PublicProperties)]
        Type inputType, JsonSerializerOptions? options = null);

    void Serialize<TValue>(Utf8JsonWriter writer, TValue value, JsonSerializerOptions? options = null);
    string Serialize<TValue>(TValue value, JsonSerializerOptions? options = null);

    Task SerializeAsync(Stream utf8Json, object? value,
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields |
                                    DynamicallyAccessedMemberTypes.PublicProperties)]
        Type inputType, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default);

    Task SerializeAsync<TValue>(Stream utf8Json, TValue value, JsonSerializerOptions? options = null,
        CancellationToken cancellationToken = default);

    byte[] SerializeToUtf8Bytes(object? value,
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields |
                                    DynamicallyAccessedMemberTypes.PublicProperties)]
        Type inputType, JsonSerializerOptions? options = null);

    byte[] SerializeToUtf8Bytes<TValue>(TValue value, JsonSerializerOptions? options = null);
}