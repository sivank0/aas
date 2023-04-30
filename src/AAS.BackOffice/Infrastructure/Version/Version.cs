#region

using System.Reflection;

#endregion

namespace AAS.BackOffice.Infrastructure.Version;

public static class Version
{
    private static readonly string AssemblyVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();

    public static string WithVersion(this string url)
    {
        return $"{url}?v={AssemblyVersion}";
    }
}