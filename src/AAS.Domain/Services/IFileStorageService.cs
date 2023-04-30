using AAS.Tools.Types.Files;
using AAS.Tools.Types.Results;

namespace AAS.Domain.Services;

public interface IFileStorageService
{
    Task<Result> SaveFiles(params FileDetailsOfBytes[] fileDetails);
    Task<Result> RemoveFiles(params String[] removeFilePaths);
}