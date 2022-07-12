using ApiPlayground.Middleware;
using ApiPlayground.services;
namespace ApiPlayground.Startup;

public class Startup
{

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddResponseCompression();
        services.AddControllers().AddNewtonsoftJson();
        services.AddTransient<IBidmasCalculator, BidmasCalculator>();
        services.AddTransient<ILeftToRightCalculator, LeftToRightCalculator>();
        services.AddTransient<IStringParsingService, StringParsingService>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime appLifetime)
    {
        app.UseRouting();
        app.UseMiddleware<ExceptionResponseMiddleware>();
        app.UseResponseCompression();
        app.UseEndpoints(x => { x.MapControllers(); });
    }



}