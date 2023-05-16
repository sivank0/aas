using AAS.Domain.Services;
using AAS.Services.FileStorage.Providers;
using AAS.Tools.Types.Files;
using AAS.Tools.Types.Results;

namespace AAS.Services.FileStorage;

public class FileStorageService : IFileStorageService
{
    private readonly IFileStorageProvider _fileStorageProvider;

    public FileStorageService(IFileStorageProvider fileStorageProvider)
    {
        _fileStorageProvider = fileStorageProvider;
    }

    public Task<Result> SaveAndRemoveFiles(FileDetailsOfBytes[] fileDetails, String[]? removeFilePaths)
    {
        return SendRequest(fileDetails, removeFilePaths ?? Array.Empty<String>());
    }

    private async Task<Result> SendRequest(FileDetailsOfBytes[] fileDetails, String[] removeFilePaths)
    {
        try
        {
            return await _fileStorageProvider.SendRequest(fileDetails, removeFilePaths);
        }
        catch
        {
            return Result.Fail("Произошла ошибка при выполнении запроса, повторите попытку ещё раз");
        }
    }
}