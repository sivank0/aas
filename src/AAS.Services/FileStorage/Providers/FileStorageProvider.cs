using System.Net.Http.Json;
using AAS.Tools.Converters;
using AAS.Tools.Json;
using AAS.Tools.Types.Files;
using AAS.Tools.Types.Results;

namespace AAS.Services.FileStorage.Providers;

public class FileStorageProvider : IFileStorageProvider
{
    private readonly HttpClient _httpClient;
    private readonly IJsonSerializer _jsonSerializer;

    public FileStorageProvider(HttpClient httpClient, IJsonSerializer jsonSerializer)
    {
        httpClient.BaseAddress = new Uri("https://localhost:44395/");
        _httpClient = httpClient;
        _jsonSerializer = jsonSerializer;
    }

    public async Task<Result> SaveFiles(FileDetailsOfBytes[] fileDetails)
    {
        try
        {
            Object data = new { FileDetails = fileDetails };

            HttpResponseMessage response =
                await _httpClient.PostAsJsonAsync("files/save", data, options: JsonTools.DefaultOptions);

            String result = await response.Content.ReadAsStringAsync();

            Result? uploadResult = _jsonSerializer.Deserialize<Result>(result);

            if (uploadResult is null) throw new ArgumentNullException("updloadResult");

            return uploadResult;
        }
        catch (Exception ex)
        {
            return Result.Fail("Не удалось получить ответ от сервера, повторите попытку ещё раз");
        }
    }

    public Task<Result> RemoveFiles(string[] removeFilePaths)
    {
        throw new NotImplementedException();
    }
}