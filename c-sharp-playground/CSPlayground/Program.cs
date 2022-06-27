// See https://aka.ms/new-console-template for more information

using Autofac;
using Autofac.Core;
using CSharpPlayground.controllers;
using CSharpPlayground.services;

namespace CSharpPlayground;

public class Program
{
    private static IContainer Container { get; set; }
    public static void Main(string[] args)
    {
        var builder = new ContainerBuilder();
        builder.RegisterType<TerminalController>().As<IProgramController>();
        builder.RegisterType<Calculator>().As<ICalculator>();
        builder.RegisterType<StringParsingService>().As<IStringParsingService>();
        Container = builder.Build();
        
        RunApplication(args);
    }

    private static void RunApplication(string[] args)
    {
        using var scope = Container.BeginLifetimeScope();
        var controller = scope.Resolve<IProgramController>();
        controller.Run();
    }
}