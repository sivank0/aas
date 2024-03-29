using System.IO.Compression;
using AAS.Configurator;
using AAS.Configurator.Extensions;
using AAS.Domain;
using AAS.FileStorage.Infrastucture;
using AAS.Services.Configurator;
using AAS.Tools.Binders;
using AAS.Tools.Json;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.FileProviders;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args); 

builder.Host.ConfigureWeb((context, serviceCollection) =>
{
    serviceCollection.Initialize(context.Configuration);
});

builder.Services.Configure<GzipCompressionProviderOptions>(options =>
    {
        options.Level = CompressionLevel.Optimal;
    })
    .AddResponseCompression(options =>
    {
        options.Providers.Add<BrotliCompressionProvider>();
        options.Providers.Add<GzipCompressionProvider>();
        options.EnableForHttps = true;
    })
    .Configure<IISServerOptions>(x =>
    {
        x.MaxRequestBodySize = 200 * 1024 * 1024;
    })
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions
            .AddJsonSettings()
            .ApplyToolsConverters();
    });

WebApplication application = builder.Build();

if (application.Environment.IsDevelopment())
    application.UseDeveloperExceptionPage();

application
    .UseResponseCompression()
    .UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(FileSystemSeparator.GetPath(Configurations.FileStorage.UploadFolder))
    })
    .UseRouting()
    .UseEndpoints(endpoints =>
    {
        endpoints.MapControllerRoute("default", "{area:exists}/{controller}/{action}");
        endpoints.MapControllers();
    });

application.Run();