using Microsoft.Extensions.FileProviders;
using System.Runtime.CompilerServices;

namespace AAS.Services.Common;
public class SqlFileProvider : EmbeddedFileProvider
{
    private static readonly SqlFileProvider Default = new();
    private readonly string _basePath;

    public static string GetQuery([CallerMemberName] string queryName = "", [CallerFilePath] string path = "", string folder = "")
    {
        return Default.Get(queryName, path, folder);
    }

    public SqlFileProvider([CallerFilePath] string basePath = "") : base(typeof(SqlFileProvider).Assembly)
    {
        _basePath = Path.GetDirectoryName(basePath) ?? string.Empty;
    }

    public string Get([CallerMemberName] string queryName = "", [CallerFilePath] string path = "", string folder = "")
    {
        string fileDirectory = (Path.GetDirectoryName(path) ?? string.Empty) + (!string.IsNullOrWhiteSpace(folder) ? $"\\{folder}" : "");
        string prefix = MakeRelativePath(_basePath, fileDirectory).Replace('\\', '.');
        string fileName = $"{prefix}.{queryName}.sql";

        using Stream stream = Default.GetFileInfo(fileName).CreateReadStream();
        using StreamReader reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }

    // https://stackoverflow.com/a/340454
    public static string MakeRelativePath(string fromPath, string toPath)
    {
        if (string.IsNullOrEmpty(fromPath)) throw new ArgumentNullException("fromPath");
        if (string.IsNullOrEmpty(toPath)) throw new ArgumentNullException("toPath");

        Uri fromUri = new Uri(fromPath);
        Uri toUri = new Uri(toPath);

        if (fromUri.Scheme != toUri.Scheme) { return toPath; } // path can't be made relative.

        Uri relativeUri = fromUri.MakeRelativeUri(toUri);
        string relativePath = Uri.UnescapeDataString(relativeUri.ToString());

        if (toUri.Scheme.Equals("file", StringComparison.InvariantCultureIgnoreCase))
        {
            relativePath = relativePath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
        }

        return relativePath;
    }
}
