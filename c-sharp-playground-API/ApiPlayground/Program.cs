// See https://aka.ms/new-console-template for more information

using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore;

namespace ApiPlayground;

public class Program
{
    
    public static void Main(string[] args)
    {

        // RunApplication(args);
        
        CreateHostBuilder(args).Build().Run();
    }

    // private static void RunApplication(string[] args)
    // {
    //     using var scope = Container.BeginLifetimeScope();
    //     var controller = scope.Resolve<IProgramController>();
    //     controller.Run();
    // }
    
     private static IWebHostBuilder CreateHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureServices(services => services.AddAutofac())
                .UseUrls("http://+:9028")
                .UseStartup<Startup.Startup>();

}