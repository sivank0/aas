using AAS.Tools.Types.IDs;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AAS.Tools.Converters;
public class IDJsonConverter : JsonConverter<ID>
{
    public override ID Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return ID.Parse(reader.GetString() ?? "");
    }

    public override void Write(Utf8JsonWriter writer, ID value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
