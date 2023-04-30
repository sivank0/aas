namespace AAS.FileStorage.Infrastucture;

public static class FileSystemSeparator
{
    public static string GetPath(string path, char currentSeparator = '/')
    {
        return currentSeparator == Path.DirectorySeparatorChar
            ? path
            : path.Replace(currentSeparator, Path.DirectorySeparatorChar);
    }
}