#region

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

#endregion

namespace AAS.Tools.Types;

public class Jsonb
{
    public string Value { get; set; }

    public Jsonb(string json)
    {
        string strInput = json.Trim();
        if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
            (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
            try
            {
                JToken obj = JToken.Parse(strInput);
                Value = obj.First.Path.Contains("Value") ? obj.Value<string>("Value") : json;
            }
            catch (JsonReaderException)
            {
                throw new JsonReaderException();
            }
        else
            throw new JsonReaderException();
    }

    public Jsonb(object value)
    {
        Value = JsonConvert.SerializeObject(value);
    }

    public override string ToString()
    {
        return Value;
    }
}