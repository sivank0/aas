using AAS.Tools.Types.Files;
using AAS.Tools.Types.Results;

namespace AAS.Services.FileStorage.Providers;

public interface IFileStorageProvider
{
    Task<Result> SendRequest(FileDetailsOfBase64[] fileDetails, String[] removeFilePaths);
}