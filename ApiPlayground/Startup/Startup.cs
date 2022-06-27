using ApiPlayground.Configuration;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace ApiPlayground.Startup;

public class Startup
{
    private IContainer ApplicationContainer { get; set; }
    public IConfiguration Configuration { get; }
    
    public Startup(IWebHostEnvironment env)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", true, true)
            .AddEnvironmentVariables();
        Configuration = builder.Build();

        // Log.Logger = new LoggerConfiguration()
        //     .ReadFrom.Configuration(Configuration)
        //     .Enrich.WithProperty("appcode", System.Reflection.Assembly.GetExecutingAssembly().GetName().Name)
        //     .Enrich.WithProperty("appversion", Configuration.GetSection("ApiVersion").Value)
        //     .CreateLogger();
    }
    
    
            // This method gets called by the runtime. Use this method to add services to the container.
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public IServiceProvider ConfigureServices(IServiceCollection services)
    {
        services.Configure<StatusSettings>(Configuration);
        services.AddResponseCompression();

        services.AddControllers();
        // services.AddLogging(loggingBuilder =>
        //     loggingBuilder.AddSerilog(dispose: true));
        var builder = AutofacConfiguration.Configure(services);

        ApplicationContainer = builder.Build();

        return new AutofacServiceProvider(ApplicationContainer);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime appLifetime)
    {
        app.UseRouting();

        app.UseResponseCompression();

        app.UseEndpoints(x => { x.MapControllers(); });
        appLifetime.ApplicationStopped.Register(() => ApplicationContainer.Dispose());
    }
}

