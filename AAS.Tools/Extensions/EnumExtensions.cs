#region

using System.ComponentModel.DataAnnotations;
using System.Reflection;

#endregion

namespace AAS.Tools.Extensions;

public static class EnumExtensions
{
    public static T ToEnum<T>(this string value)
    {
        return (T)Enum.Parse(typeof(T), value, true);
    }

    public static string GetDisplayName(this Enum source)
    {
        FieldInfo field = source.GetType().GetField(source.ToString());
        if (field == null) return source.ToString();

        DisplayAttribute attribute = field.GetCustomAttribute<DisplayAttribute>(false);
        if (attribute == null) return source.ToString();

        return attribute.GetName();
    }
}

public static class Enum<T> where T : Enum
{
    public static T[] AllCases => Enum.GetValues(typeof(T)).Cast<T>().ToArray();

    public static T[] GetArray(params T[] exclude)
    {
        if (!typeof(T).IsEnum) throw new ArgumentException("Required EnumType");

        return Enum.GetValues(typeof(T)).OfType<T>().Where(x => !exclude.Contains(x)).ToArray();
    }
}