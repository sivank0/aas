using System.Reflection;
using AAS.Tools.Converters;
using AAS.Tools.DB;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AAS.Configurator.Extensions;

public static class HostBuilderExtensions
{
	const String ENVIRONMENT_NAME = "ASPNETCORE_ENVIRONMENT";
	private static void DefaultAppConfiguration(HostBuilderContext context, IConfigurationBuilder builder)
	{
		builder
			.SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
			.AddJsonFile($"configs/connectionString.json", optional: true, reloadOnChange: true)
			.AddEnvironmentVariables();
	}

	private static void DefaultConfigureServices(HostBuilderContext context, IServiceCollection serviceCollection)
	{
		IConfiguration configuration = context.Configuration;

		IJsonSerializer jsonSerializer = new TextJsonSerializer();

		serviceCollection
			.AddSingleton<IJsonSerializer>(jsonSerializer);
	}

	public static IHostBuilder ConfigureWeb(this IHostBuilder hostBuilder, Action<HostBuilderContext, IServiceCollection>? configureServiceCollection = null)
	{
		NpsqlConfigurator.Configure();

		hostBuilder.ConfigureAppConfiguration(DefaultAppConfiguration);
		hostBuilder.ConfigureServices((context, serviceCollection) =>
		{
			DefaultConfigureServices(context, serviceCollection); 
			configureServiceCollection?.Invoke(context, serviceCollection);
		});

		return hostBuilder;
	}

	public static IHostBuilder ConfigureConsole(this IHostBuilder hostBuilder, Action<HostBuilderContext, IServiceCollection>? configureServiceCollection = null, String? environment = null)
	{
		environment = environment ?? Environment.GetEnvironmentVariable(ENVIRONMENT_NAME);
		hostBuilder.UseEnvironment(environment);

		NpsqlConfigurator.Configure();

		hostBuilder.ConfigureAppConfiguration((context, configurationBuilder) =>
		{
			configurationBuilder.AddCommandLine(Environment.GetCommandLineArgs());
			DefaultAppConfiguration(context, configurationBuilder);
		});

		hostBuilder.ConfigureAppConfiguration((hostContext, configApp) =>
		{
			configApp.AddCommandLine(Environment.GetCommandLineArgs());
		});

		hostBuilder.ConfigureServices(((context, serviceCollection) =>
		{
			DefaultConfigureServices(context, serviceCollection); 
			configureServiceCollection?.Invoke(context, serviceCollection);
		}));

		hostBuilder.ConfigureLogging((hostContext, configLogging) => configLogging.AddConsole()); 
		hostBuilder.UseConsoleLifetime();

		return hostBuilder;
	}
}
