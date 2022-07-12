// See https://aka.ms/new-console-template for more information

using System;
using System.Net.Http;
using System.Threading.Tasks;
using Autofac;
using CSPlayground.controllers;
using CSPlayground.services;

namespace CSPlayground;

public class Program
{
    private static IContainer Container { get; set; }
    public static void Main(string[] args)
    {
        var builder = new ContainerBuilder();
        builder.RegisterType<ApiController>().As<IProgramController>();
        if (string.Equals(args[0], "bidmas", StringComparison.CurrentCultureIgnoreCase))
        {
            builder.RegisterType<BidmasCalculator>().As<ICalculator>();
        }
        else
        {
            builder.RegisterType<Calculator>().As<ICalculator>();
        }
        builder.RegisterType<StringParsingService>().As<IStringParsingService>();
        builder.RegisterType<HttpClient>().AsSelf();
        builder.RegisterType<ApiCalculator>().As<IApiCalculator>();
        Container = builder.Build();
        
        
        Task t = MainAsync(args);
        t.Wait();
    }
    
    static async Task MainAsync(string[] args)
    {
        await RunApplication(args);
    }

    private static async Task RunApplication(string[] args)
    {
        string path = args[0] == String.Empty ? null : args[0];
        await using var scope = Container.BeginLifetimeScope();
        var controller = scope.Resolve<IProgramController>();
        await controller.Run(path);
    }
}