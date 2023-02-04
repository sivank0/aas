using AAS.Tools.Types;
using AAS.Tools.Types.IDs;

namespace AAS.Tools.DB;

public static class MapperValue
{
    public static object GetValue(object value, Type targetType)
    {
        if (value == DBNull.Value || value == null)
            return null;

        Type type = Nullable.GetUnderlyingType(targetType) ?? targetType;

        if (type.IsArray)
            return GetArrayOfValues(value, type);

        if (type.IsEnum)
            return Enum.ToObject(type, value);

        if (type == typeof(ID))
            return new ID(value as byte[]);

        if (type == typeof(Jsonb))
            return new Jsonb(value.ToString());

        return Convert.ChangeType(value, type);
    }

    public static object GetArrayOfValues(object values, Type type)
    {
        Type elementType = type.GetElementType();

        if (elementType.IsEnum)
        {
            object[] array = ((Array)values).Cast<int>().Select(x => Enum.ToObject(elementType, x)).ToArray();
            return elementType.GetArray(array);
        }

        if (elementType == typeof(ID))
        {
            return ((Array)values).Cast<byte[]>().Select(x => new ID(x)).ToArray();
        }
        if (elementType == typeof(Jsonb))
        {
            return ((Array)values).Cast<Jsonb>().ToArray();
        }

        return Convert.ChangeType(values, type);
    }
}
