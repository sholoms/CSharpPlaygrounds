using FileApi.Services;
using FileApi.Services.interfaces;

namespace FileApi.Startup;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IWebHostEnvironment env)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", true, true)
            .AddEnvironmentVariables();
        Configuration = builder.Build();
    }
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddResponseCompression();
        services.AddControllers().AddNewtonsoftJson();
        services.AddTransient<IFileService, FileService>();

        services.AddSwaggerGen();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime appLifetime)
    {
        app.UseRouting();
        app.UseResponseCompression();
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseEndpoints(x => { x.MapControllers(); });
    }
}
