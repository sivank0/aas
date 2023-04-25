#region

using AAS.BackOffice.Filters;
using AAS.Domain;
using AAS.Services.Configurator;
using AAS.Tools.Binders;
using AAS.Tools.Json;
using Microsoft.AspNetCore.ResponseCompression;

#endregion

namespace AAS.BackOffice;

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

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddResponseCompression(options =>
        {
            options.EnableForHttps = true;
            options.Providers.Add<BrotliCompressionProvider>();
            options.Providers.Add<GzipCompressionProvider>();
        });

        services.AddControllersWithViews(mvcOptions =>
            {
                mvcOptions.EnableEndpointRouting = false;
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

        services.Initialize(_configuration);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

        app.UseResponseCompression();
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.UseMvc(routes =>
        {
            routes.MapRoute(
                "default",
                "{controller=Home}/{action=Index}/{id?}");
        });
    }
}