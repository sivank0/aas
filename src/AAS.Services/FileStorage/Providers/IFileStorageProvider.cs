using AAS.Tools.Types.Files;
using AAS.Tools.Types.Results;

namespace AAS.Services.FileStorage.Providers;

public interface IFileStorageProvider
{
    public Task<Result> SaveFiles(FileDetailsOfBytes[] fileDetails);
    public Task<Result> RemoveFiles(String[] removeFilePaths);
}