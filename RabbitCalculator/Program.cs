using Autofac;
using RabbitCalculator.Handlers;

namespace RabbitCalculator;

class RPCServer
{
    private static IContainer Container { get; set; }
    public static void Main(string[] args)
    {
        var builder = new ContainerBuilder();
        builder.RegisterType<HttpClient>().AsSelf();
        builder.RegisterType<ApiCalculatorClient>().As<IApiCalculatorClient>().WithParameter("baseUrl", "http://localhost:9028/");
        builder.RegisterType<CalculatorHandler>().As<ICalculatorHandler>();
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
        var handler = scope.Resolve<ICalculatorHandler>();
        await handler.Run();
    }
}