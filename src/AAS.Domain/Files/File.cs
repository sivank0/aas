using AAS.Configurator;

namespace AAS.Domain.Files;

public class File
{
    public String Path { get; private set; }
    public String? Url => GetFileUrl();

    private String? GetFileUrl()
    {
        if (String.IsNullOrWhiteSpace(Path)) return null;

        String fileStorageHost = Configurations.FileStorage.Host;

        if (!fileStorageHost.EndsWith("/"))
        {
            if (!Path.StartsWith("/"))
                fileStorageHost = String.Concat(fileStorageHost, "/");
        }
        else
        {
            if (Path.StartsWith("/"))
                Path = Path.Skip(1).ToString();
        }

        return String.Concat(fileStorageHost, Path);
    }

    public File(string path)
    {
        Path = path;
    }
}
