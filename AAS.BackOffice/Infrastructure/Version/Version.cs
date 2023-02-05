using System.Reflection;

namespace AAS.BackOffice.Infrastructure.Version;

public static class Version
{
    private static readonly String AssemblyVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
    public static String WithVersion(this String url) => $"{url}?v={AssemblyVersion}";
}
