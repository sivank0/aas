#region

using AAS.Tools.Types.IDs;

#endregion

namespace AAS.Tools.DB;

internal static class ReflectionHelper
{
    private static readonly Type[] SimpleTypes =
    {
        typeof(ID),
        typeof(decimal),
        typeof(string),
        typeof(Guid),
        typeof(DateTime)
    };

    public static bool IsSimpleType(Type type)
    {
        return type.IsEnum
               || type.IsPrimitive
               || SimpleTypes.Contains(type);
    }

    public static Array GetArray(this Type elementType, IReadOnlyList<object> objects)
    {
        Array array = Array.CreateInstance(elementType, objects.Count);
        for (int i = 0; i < array.Length; ++i)
            array.SetValue(objects[i], i);

        return array;
    }
}