using AAS.Domain.Files.Enums;
using AAS.Domain.Files.Enums.Extensions;
using AAS.Tools.Types.Files;
using AAS.Tools.Types.IDs;
using PMS.Tools.Types;

namespace AAS.Domain.Files;

public class FileBlank
{
    public String Name { get; set; }
    public String? Path { get; set; }
    public String Base64 { get; set; }
    public FileArea Area { get; set; }
    public FileState State { get; set; }

    public FileBlank(string name, string path, string base64, FileArea area, FileState state)
    {
        Name = name;
        Path = path;
        Base64 = base64;
        Area = area;
        State = state;
    }

    public static (FileDetailsOfBytes[] fileDetails, string[] removeFilePaths) GetFileDetails(FileBlank fileBlank,
        ID? bidId = null, ID? userId = null)
    {
        List<FileDetailsOfBytes> fileDetails = new List<FileDetailsOfBytes>();
        List<String> removeFilePaths = new List<String>();

        switch (fileBlank.State)
        {
            case FileState.Added:
                DataUrl? dataUrl = DataUrl.Parse(fileBlank.Base64);

                if (dataUrl == null) throw new Exception("Один из файлов пустой или имеет невалидное содержимое.");

                ID? fileParentId = fileBlank.Area == FileArea.Bid
                    ? bidId
                    : userId;

                if (fileParentId is null) throw new ArgumentNullException(nameof(fileParentId));

                String path = String.Concat(fileBlank.Area.GetFileDirectory(), fileParentId, "/");

                FileDetailsOfBytes fileDetail = new FileDetailsOfBytes(
                    fileBlank.Name,
                    dataUrl.Extension,
                    dataUrl.ToByteArray(),
                    path: path
                );
                fileBlank.Path = fileDetail.FullPath;

                fileDetails.Add(fileDetail);
                break;

            case FileState.Intact: break;

            case FileState.Removed:
                if (String.IsNullOrWhiteSpace(fileBlank.Path)) throw new ArgumentNullException("Invalid image url");

                removeFilePaths.Add(fileBlank.Path);
                fileBlank.Path = null;
                break;

            default: throw new InvalidOperationException("Invalid product image state");
        }

        return (fileDetails.ToArray(), removeFilePaths.ToArray());
    }
}