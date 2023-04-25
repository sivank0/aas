#region

using AAS.FileStorage.Infrastucture;
using AAS.Tools.Types.Files;
using AAS.Tools.Types.Results;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace AAS.FileStorage.Areas;

public class FileController : Controller
{
    private string GetDirectory(string path)
    {
        int rightPositionSeparator = path.Length;

        for (int i = path.Length - 1; i >= 0; i--)
        {
            if (path[i] != '/' && path[i] != '\\') continue;

            rightPositionSeparator = i + 1;
            break;
        }

        return path.Substring(0, rightPositionSeparator);
    }

    public record FileStorageRequest(FileDetails[] FileDetails, string[] FilePathsForDelete);

    [HttpPost("files/upload")]
    public async Task<Result> Upload([FromBody] FileStorageRequest request)
    {
        Result result = RemoveFiles(request.FilePathsForDelete);

        result = await SaveFiles(request.FileDetails);

        return result;
    }

    private async Task<Result> SaveFiles(FileDetails[] fileDetails)
    {
        List<Error> errors = new List<Error>();

        foreach (FileDetails fileDetail in fileDetails)
        {
            try
            {
                await using MemoryStream ms = fileDetail switch
                {
                    FileDetailsOfBytes fileDetailsOfBytes => new MemoryStream(fileDetailsOfBytes.Bytes),
                    FileDetailsOfBase64 fileDetailsOfBase64 => new MemoryStream(byte.Parse(fileDetailsOfBase64.Base64)),
                    _ => throw new Exception()
                };

                if (ms.Length == 0) throw new Exception("MemoryStream is empty");

                string fullPath = $"C:/FileStorage/AAS/{fileDetail.Path}";
                string dir = FileSystemSeparator.GetPath(GetDirectory(fullPath));

                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

                await System.IO.File.WriteAllBytesAsync(FileSystemSeparator.GetPath(fullPath), ms.ToArray());
            }
            catch
            {
                errors.Add(new Error("Не удалось сохранить файл, повторите попытку ещё раз"));
            }
        }

        return errors.Count != 0
            ? Result.Fail(errors[0].Message)
            : Result.Success();
    }

    private Result RemoveFiles(string[] filePathsForDelete)
    {
        List<Error> errors = new List<Error>();

        foreach (string filePath in filePathsForDelete)
        {
            try
            {
                string fullPath = $"C:/FileStorage/AAS/{filePath}";
                string dir = FileSystemSeparator.GetPath(GetDirectory(fullPath));

                if (!Directory.Exists(dir)) throw new Exception("Removing file directory is not exist");

                System.IO.File.Delete(FileSystemSeparator.GetPath(fullPath));
            }
            catch
            {
                errors.Add(new Error("Не удалось удалить файл, повторите попытку ещё раз"));
            }
        }

        return errors.Count != 0
            ? Result.Fail(errors[0].Message)
            : Result.Success();
    }
}