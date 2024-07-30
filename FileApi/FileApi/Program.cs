// See https://aka.ms/new-console-template for more information

using Microsoft.AspNetCore;

namespace FileApi;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    private static IWebHostBuilder CreateHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://+:9028")
                .UseStartup<Startup.Startup>();
}