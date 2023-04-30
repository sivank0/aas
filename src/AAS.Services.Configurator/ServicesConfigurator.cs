#region

using AAS.Domain.Services;
using AAS.Services.Bids;
using AAS.Services.Bids.Repositories;
using AAS.Services.FileStorage;
using AAS.Services.FileStorage.Providers;
using AAS.Services.Users;
using AAS.Services.Users.Repositories;
using AAS.Tools.Converters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace AAS.Services.Configurator;

public static class ServicesConfigurator
{
    public static IServiceCollection Initialize(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetSection("ConnectionString").Value;

        if (string.IsNullOrWhiteSpace(connectionString))
            throw new Exception("[ServicesConfigurator] parameter 'connectionString' is null");

        TextJsonSerializer jsonSerializer = new();
        services.AddSingleton<IJsonSerializer>(jsonSerializer);

        #region Providers

        services.AddHttpClient<IFileStorageProvider, FileStorageProvider>();

        #endregion

        #region Repositories

        services.AddSingleton<IUsersRepository>(new UsersRepository(connectionString));
        services.AddSingleton<IBidsRepository>(new BidsRepository(connectionString));

        #endregion Repositories

        #region Services

        services.AddSingleton<IUsersService, UsersService>();
        services.AddSingleton<IFileStorageService, FileStorageService>();
        services.AddSingleton<IBidsService, BidsService>();

        #endregion Services

        services.BuildServiceProvider();
        return services;
    }
}