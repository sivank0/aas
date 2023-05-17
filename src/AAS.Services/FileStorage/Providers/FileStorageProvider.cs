using System.Net.Http.Json;
using System.Text;
using AAS.Configurator;
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
        httpClient.BaseAddress = new Uri(Configurations.FileStorage.Host);
        _httpClient = httpClient;
        _jsonSerializer = jsonSerializer;
    }

    public async Task<Result> SendRequest(FileDetailsOfBytes[] fileDetails, String[] removeFilePaths)
    {
        try
        {
            Object data = new { FileDetails = fileDetails, RemoveFilePaths = removeFilePaths };

            HttpContent httpContent =
                new StringContent(_jsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync("/files/upload", httpContent);

            String result = await response.Content.ReadAsStringAsync();

            Result? uploadResult = _jsonSerializer.Deserialize<Result>(result);

            if (uploadResult is null) throw new ArgumentNullException("updloadResult");

            return uploadResult;
        }
        catch
        {
            return Result.Fail("Не удалось получить ответ от сервера, повторите попытку ещё раз");
        }
    }
}