#region

using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

#endregion

namespace AAS.Tools.Converters;

public interface IJsonSerializer
{
    JsonSerializerOptions Options { get; }

    Object? Deserialize(ReadOnlySpan<Byte> utf8Json,
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors |
                                    DynamicallyAccessedMemberTypes.PublicFields |
                                    DynamicallyAccessedMemberTypes.PublicProperties)]
        Type returnType, JsonSerializerOptions? options = null);

    Object? Deserialize(String json,
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors |
                                    DynamicallyAccessedMemberTypes.PublicFields |
                                    DynamicallyAccessedMemberTypes.PublicProperties)]
        Type returnType, JsonSerializerOptions? options = null);

    Object? Deserialize(ref Utf8JsonReader reader,
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors |
                                    DynamicallyAccessedMemberTypes.PublicFields |
                                    DynamicallyAccessedMemberTypes.PublicProperties)]
        Type returnType, JsonSerializerOptions? options = null);

    TValue? Deserialize<TValue>(ReadOnlySpan<Byte> utf8Json, JsonSerializerOptions? options = null);
    TValue? Deserialize<TValue>(String json, JsonSerializerOptions? options = null);
    TValue? Deserialize<TValue>(JsonElement element, JsonSerializerOptions? options = null);
    TValue? Deserialize<TValue>(ref Utf8JsonReader reader, JsonSerializerOptions? options = null);

    ValueTask<Object?> DeserializeAsync(Stream utf8Json,
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors |
                                    DynamicallyAccessedMemberTypes.PublicFields |
                                    DynamicallyAccessedMemberTypes.PublicProperties)]
        Type returnType, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default);

    ValueTask<TValue?> DeserializeAsync<TValue>(Stream utf8Json, JsonSerializerOptions? options = null,
        CancellationToken cancellationToken = default);

    String Serialize(Object? value,
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields |
                                    DynamicallyAccessedMemberTypes.PublicProperties)]
        Type inputType, JsonSerializerOptions? options = null);

    void Serialize(Utf8JsonWriter writer, Object? value,
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields |
                                    DynamicallyAccessedMemberTypes.PublicProperties)]
        Type inputType, JsonSerializerOptions? options = null);

    void Serialize<TValue>(Utf8JsonWriter writer, TValue value, JsonSerializerOptions? options = null);
    String Serialize<TValue>(TValue value, JsonSerializerOptions? options = null);

    Task SerializeAsync(Stream utf8Json, Object? value,
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields |
                                    DynamicallyAccessedMemberTypes.PublicProperties)]
        Type inputType, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default);

    Task SerializeAsync<TValue>(Stream utf8Json, TValue value, JsonSerializerOptions? options = null,
        CancellationToken cancellationToken = default);

    Byte[] SerializeToUtf8Bytes(Object? value,
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields |
                                    DynamicallyAccessedMemberTypes.PublicProperties)]
        Type inputType, JsonSerializerOptions? options = null);

    Byte[] SerializeToUtf8Bytes<TValue>(TValue value, JsonSerializerOptions? options = null);
}