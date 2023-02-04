using AAS.Domain.Services;
using AAS.Services.Users;
using AAS.Services.Users.Repositories;
using AAS.Tools.Converters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AAS.Services.Configurator;

public static class ServicesConfigurator
{
    public static void Initialize(this IServiceCollection services, IConfiguration configuration)
    {
        String? connectionString = configuration.GetSection("ConnectionString").Value;

        if (String.IsNullOrWhiteSpace(connectionString))
            throw new Exception("[ServicesConfigurator] parameter 'connectionString' is null");

        TextJsonSerializer jsonSerializer = new();
        services.AddSingleton<IJsonSerializer>(jsonSerializer);

        #region Repositories

        services.AddSingleton<IUsersRepository>(new UsersRepository(connectionString));

        #endregion Repositories

        #region Services  

        services.AddSingleton<IUsersService, UsersService>();

        #endregion Services 

        services.AddSingleton(configuration);
        services.BuildServiceProvider();
    }
}
