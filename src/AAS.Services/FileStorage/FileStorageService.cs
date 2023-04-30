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
    
    public async Task<Result> SaveFiles(params FileDetailsOfBytes[] fileDetails)
    {
        try
        {
            return await _fileStorageProvider.SaveFiles(fileDetails);
        }
        catch
        {
            return Result.Fail("Сохранение файлов произошло с ошибками");
        }
    }

    public async Task<Result> RemoveFiles(params string[] removeFilePaths)
    {
        try
        {
            return await _fileStorageProvider.RemoveFiles(removeFilePaths);
        }
        catch
        {
            return Result.Fail("Удаление файлов произошло с ошибками");
        }
    }
}