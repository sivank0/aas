using AAS.Tools.Types.Files;
using AAS.Tools.Types.Results;

namespace AAS.Domain.Services;

public interface IFileStorageService
{
    Task<Result> SaveAndRemoveFiles(FileDetailsOfBytes[] fileDetails, String[]? removeFilePaths = null);
}