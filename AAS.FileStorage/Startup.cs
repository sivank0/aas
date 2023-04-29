#region

using System.IO.Compression;
using AAS.FileStorage.Infrastucture;
using AAS.Tools.Json;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.FileProviders;

#endregion

namespace PD.FileStorage;

public class Startup
{
    private readonly IWebHostEnvironment _environment;
    private readonly IConfiguration _configuration;

    public Startup(IWebHostEnvironment environment)
    {
        _environment = environment;
        _configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"Configurations/DbConnections/connectionStrings.{environment.EnvironmentName}.json")
            .Build();
    }

    public void ConfigureServices(IServiceCollection services)
    {
        string myAllowSpecificOrigins = "_myAllowSpecificOrigins";
        services.AddCors(options =>
        {
            options.AddPolicy(myAllowSpecificOrigins,
                builder =>
                {
                    builder
                        .WithOrigins("https://localhost:44377")
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .SetIsOriginAllowed((host) => true)
                        .AllowAnyHeader();
                });
        });

        services.Configure<GzipCompressionProviderOptions>(options => { options.Level = CompressionLevel.Optimal; });

        services.AddResponseCompression(options =>
        {
            options.Providers.Add<GzipCompressionProvider>();
            options.EnableForHttps = true;
        });

        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions
                .AddJsonSettings()
                .ApplyToolsConverters();
        });


        services.Configure<IISServerOptions>(x => { x.MaxRequestBodySize = 200 * 1024 * 1024; });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseCors("_myAllowSpecificOrigins");

        if (env.IsDevelopment())
            app.UseDeveloperExceptionPage();

        app.UseResponseCompression();

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(FileSystemSeparator.GetPath("C:/FileStorage/AAS"))
        });

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute("default", "{area:exists}/{controller}/{action}");
            endpoints.MapControllers();
        });
    }
}