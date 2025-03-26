using Autofac;
using FileReceiverAndSender.controllers;
using FileReceiverAndSender.Services;
using File = FileReceiverAndSender.Services.File;

namespace FileReceiverAndSender;

public class Program
{
  private static IContainer Container { get; set; }
  public static void Main(string[] args)
  {
    var builder = new ContainerBuilder();
    builder.RegisterType<FileController>().As<IProgramController>();
    builder.RegisterType<File>().As<IRabbitCalculator>();
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
    await using var scope = Container.BeginLifetimeScope();
    var controller = scope.Resolve<IProgramController>();
    await controller.Run();
  }
}