using ApiPlayground.Configuration;
using ApiPlayground.controllers;
using ApiPlayground.Middleware;
using ApiPlayground.services;
using ApiPlayground.services.interfaces;
using Microsoft.Extensions.Configuration;

namespace ApiPlayground.Startup;

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
        using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
        ILogger logger = factory.CreateLogger("Program");
        services.AddResponseCompression();
        services.AddControllers().AddNewtonsoftJson();
        services.AddLogging(builder => builder.AddConsole());
        services.AddTransient<ICalculatorController, CalculatorControllerImplementation>();
        services.AddTransient<IBidmasCalculator, BidmasCalculator>();
        services.AddTransient<ILeftToRightCalculator, LeftToRightCalculator>();
        services.AddTransient<IStringParsingService, StringParsingService>();
        services.AddTransient<IFileService, FileService>();
        services.Configure<FileSettings>(settings =>
        {
            settings.FilePath = Configuration.GetValue<string>("filepath");
            settings.DockerTest = Configuration.GetValue<string>("dockertest");
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime appLifetime)
    {
        app.UseRouting();
        app.UseMiddleware<ExceptionResponseMiddleware>();
        app.UseResponseCompression();
        app.UseEndpoints(x => { x.MapControllers(); });
    }



}
