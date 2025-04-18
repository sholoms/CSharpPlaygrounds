﻿using Autofac;
using RabbitCalculatorConsole.controllers;
using RabbitCalculatorConsole.Services;
using WriteToFileRabbitConsole.Services;

namespace WriteToFileRabbitConsole;

public class Program
{
  private static IContainer Container { get; set; }
  public static void Main(string[] args)
  {
    var builder = new ContainerBuilder();
    builder.RegisterType<RabbitController>().As<IProgramController>();
    builder.RegisterType<RabbitCalculator>().As<IRabbitCalculator>();
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