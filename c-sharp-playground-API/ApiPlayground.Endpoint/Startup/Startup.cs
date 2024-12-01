using ApiPlayground.Configuration;
using ApiPlayground.controllers;
using ApiPlayground.Handlers;
using ApiPlayground.Helpers;
using ApiPlayground.Middleware;
using ApiPlayground.RabbitConfig;
using ApiPlayground.services;
using ApiPlayground.services.interfaces;
using DbPlayground;
using DbPlayground.Persistence;
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
        services.AddResponseCompression();
        services.AddControllers().AddNewtonsoftJson();
        services.AddLogging(builder => builder.AddConsole());
        services.AddTransient<ICalculatorController, CalculatorControllerImplementation>();
        services.AddTransient<IBidmasCalculator, BidmasCalculator>();
        services.AddTransient<ILeftToRightCalculator, LeftToRightCalculator>();
        services.AddTransient<IStringParsingService, StringParsingService>();
        services.AddTransient<IFileService, FileService>();
        services.AddTransient<ICalculatorHelper, CalculatorHelper>();
        services.Configure<FileSettings>(settings =>
        {
            settings.FilePath = Configuration.GetValue<string>("filepath");
            settings.DockerTest = Configuration.GetValue<string>("dockertest");
        });
        services.Configure<RabbitSettings>(settings =>
        {
            settings.Host = Configuration.GetValue<string>("RabbitHost");
            settings.Port = Configuration.GetValue<int>("RabbitPort");
        });
        Console.WriteLine( Configuration.GetValue<string>("RabbitHost"));
        services.AddSingleton<IRabbitConnectionService, RabbitConnectionService>();
        services.AddSingleton<IRabbitMessageHandler, RabbitMessageHandler>();
        services.AddSingleton<IRabbitMessageSender, RabbitMessageSender>();
        services.AddHostedService<WriteToFileConsumerHostedService>();
        services.AddDbContext<CalculatorContext>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime appLifetime)
    {
        app.UseRouting();
        app.UseMiddleware<ExceptionResponseMiddleware>();
        app.UseResponseCompression();
        app.UseEndpoints(x => { x.MapControllers(); });
    }
}