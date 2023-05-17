using AAS.Configurator;

namespace AAS.BackOffice.Infrastructure;

public class BaseConfig
{
    public static String FileStorageHost => Configurations.FileStorage.Host;

    public static BaseConfig Current { get; } = new();
}