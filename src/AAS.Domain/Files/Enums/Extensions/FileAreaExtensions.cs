namespace AAS.Domain.Files.Enums.Extensions;

public static class FileAreaExtensions
{
    public static String GetFileDirectory(this FileArea fileArea)
    {
        return fileArea switch
        {
            FileArea.User => "Users/",
            FileArea.Bid => "Bids/",
            _ => "Files/"
        };
    }
}