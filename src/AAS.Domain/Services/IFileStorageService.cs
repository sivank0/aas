using AAS.Tools.Types.Files;
using AAS.Tools.Types.Results;

namespace AAS.Domain.Services;

public interface IFileStorageService
{
    Task<Result> SaveAndRemoveFiles(FileDetailsOfBase64[] fileDetails, String[]? removeFilePaths = null);
}