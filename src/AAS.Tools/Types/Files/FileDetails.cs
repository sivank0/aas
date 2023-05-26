#region

using System.Text.Json.Serialization;

#endregion

namespace AAS.Tools.Types.Files;

public abstract class FileDetails
{
    public string Name { get; }
    public string Extension { get; }
    public string? ContentType { get; }
    public string? Path { get; }

    [JsonIgnore] public string? FullPath => GetFullPath();

    [JsonConstructor]
    public FileDetails(string name, string extension, string? contentType = null, string? path = null)
    {
        Name = name;
        Extension = extension;
        ContentType = contentType;
        Path = path;
    }

    private string? GetFullPath()
    {
        if (string.IsNullOrWhiteSpace(Path)) return null;

        if (Path.EndsWith(Extension))
            return Path;

        return string.Concat(Path, Name, Extension);
    }
}

public class FileDetailsOfBytes : FileDetails
{
    public byte[] Bytes { get; }

    [JsonConstructor]
    public FileDetailsOfBytes(string name, string extension, byte[] bytes, string? contentType = null,
        string? path = null)
        : base(name, extension, contentType, path)
    {
        Bytes = bytes;
    }

    public FileDetailsOfBase64 AsBase64()
    {
        string base64String = Convert.ToBase64String(Bytes);

        return new FileDetailsOfBase64(Name, Extension, base64String, ContentType, Path);
    }
}

public class FileDetailsOfBase64 : FileDetails
{
    public string Base64 { get; }

    [JsonConstructor]
    public FileDetailsOfBase64(string name, string extension, string base64, string? contentType = null,
        string? path = null)
        : base(name, extension, contentType, path)
    {
        Base64 = base64;
    }
}