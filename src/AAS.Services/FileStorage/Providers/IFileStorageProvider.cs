using AAS.Tools.Types.Files;
using AAS.Tools.Types.Results;

namespace AAS.Services.FileStorage.Providers;

public interface IFileStorageProvider
{
    Task<Result> SendRequest(FileDetailsOfBytes[] fileDetails, String[] removeFilePaths);
}