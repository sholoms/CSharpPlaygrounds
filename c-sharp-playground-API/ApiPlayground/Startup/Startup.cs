using ApiPlayground.controllers;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using CSharpPlayground.services;

namespace ApiPlayground.Startup;

public class Startup
{
    private static IContainer ApplicationContainer { get; set; }

    public IServiceProvider ConfigureServices(IServiceCollection services)
    {

        services.AddResponseCompression();
        services.AddControllers();
            
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