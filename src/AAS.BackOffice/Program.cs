using AAS.BackOffice.Filters;
using AAS.Configurator.Extensions;
using AAS.Domain;
using AAS.Services.Configurator;
using AAS.Tools.Binders;
using AAS.Tools.Json;
using Microsoft.AspNetCore.ResponseCompression;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureWeb()
    .ConfigureServices((context, services) => { services.Initialize(context.Configuration); });

builder.Services
    .AddResponseCompression(options =>
    {
        options.EnableForHttps = true;
        options.Providers.Add<BrotliCompressionProvider>();
        options.Providers.Add<GzipCompressionProvider>();
    })
    .AddControllersWithViews(mvcOptions =>
    {
        mvcOptions.Filters.Add<IsAuthorizedFilter>();
        mvcOptions.ModelBinderProviders.Insert(0, new IDModelBinderProvider());
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions
            .AddJsonSettings()
            .ApplyToolsConverters()
            .ApplyAnyTypeConverters(DomainAssembly.Itself);
    });


WebApplication application = builder.Build();

if (application.Environment.IsDevelopment())
    application.UseDeveloperExceptionPage();

application
    .UseResponseCompression()
    .UseHttpsRedirection()
    .UseStaticFiles()
    .UseRouting()
    .UseEndpoints(endpoints => { endpoints.MapControllers(); });

application.Run();