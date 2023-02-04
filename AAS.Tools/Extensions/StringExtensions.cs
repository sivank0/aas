namespace AAS.Tools.Extensions;

public static class StringExtensions
{
    public static string ToCamelCase(this string value)
    {
        return value[0].ToString().ToLower() + value.Substring(1);
    }
}
