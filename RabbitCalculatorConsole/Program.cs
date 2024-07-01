using Autofac;
using Emn.DataAccess;
using RabbitCalculatorConsole.controllers;
using RabbitCalculatorConsole.Services;

namespace RabbitCalculatorConsole;

public class Program
{
    // private static IContainer Container { get; set; }
    // public static void Main(string[] args)
    // {
    //     var builder = new ContainerBuilder();
    //     builder.RegisterType<RabbitController>().As<IProgramController>();
    //     builder.RegisterType<RabbitCalculator>().As<IRabbitCalculator>();
    //     Container = builder.Build();
    //     
    //     
    //     Task t = MainAsync(args);
    //     t.Wait();
    // }
    //
    // static async Task MainAsync(string[] args)
    // {
    //     await RunApplication(args);
    // }
    //
    // private static async Task RunApplication(string[] args)
    // { 
    //     await using var scope = Container.BeginLifetimeScope();
    //     var controller = scope.Resolve<IProgramController>();
    //     await controller.Run();
    // }
    public static void Main(string[] args)
    {
        Console.WriteLine("Please write the sum to calculate");
        Console.WriteLine("type 'q' to quit");
        var input = Console.ReadLine();
        // Calculator calculator = new ();
        while (input != "q")
        {
            Console.WriteLine("---------------------");
            // var result = _calculator.Calculate(input);
            var result = input?.Encrypt();
            // var result = await _apiCalculator.Calculate(input);
            Console.WriteLine("ahs1PeuyjxBcjc1a0+1wqjUHeBJXORIhSNx43NaJGXtERzaoofSAwWgr2Hk30i/i8DYaBf6tLv+oi0kgYnT+BqazaIqQsFvr5e8J7XW/zrmx9T8OgqXaHYOQd3OK9ImlG1Gu2xAEYKWCKcKLMZS3iPgjvVr34to4bb1009vusK9DkDZevQwpmuorG1KvH+Rb9DeZNwYgeLmyoC+i6OvnhZANBsi9lupbxg4D8XC71I2pVtXAw+7SHB8HAtGI2Dc4".Decrypt());
            Console.WriteLine("ahs1PeuyjxBcjc1a0+1wqkPBzUgUXJYfv3o1eKwPdiUg/URsYOChylhBJ27fsCGEBGYloeS8ENmae7Ubl1aJ9yS8zQLhYdIh/IQnBRsHNPI3oNjeLo5UaXvWe4h6Bhl3gP1t8Etgi7ZvLsf9XxgSHYsSsA4GvmoDRyLFFZ1DdZx6XsAfuq1bxBpYsBP9kOoWYTST8BufFw1CpmuelwEa/sUpRa7OKyK/3/PUEGWxhyzP1ILUk4/Olqh/dhezlpus".Decrypt());
            // Console.WriteLine("---------------------");
            // Console.WriteLine("Please write the sum to calculate");
            // Console.WriteLine("type 'q' to quit");
            
            input = Console.ReadLine();
        }

        //_apiCalculator.close();
    }
}