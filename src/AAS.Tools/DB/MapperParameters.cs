#region

using System.Collections;
using AAS.Tools.Types.IDs;

#endregion

namespace AAS.Tools.DB;

internal static class MapperParameters
{
    public static object GetParameterValue(Type type, object value)
    {
        if (type.IsArray) return GetArrayOfValues(type.GetElementType(), (IList)value);

        if (type == typeof(ID)) return ((ID)value).ToByteArray();

        if (type.IsEnum) return (int)value;

        return value;
    }

    private static object GetArrayOfValues(Type type, IList value)
    {
        if (type == typeof(ID)) return value.Cast<ID>().Select(x => x.ToByteArray()).ToArray();

        if (type.IsEnum) return value.Cast<int>().ToArray();

        return value;
    }
}