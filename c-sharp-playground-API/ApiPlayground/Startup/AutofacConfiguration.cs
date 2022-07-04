using ApiPlayground.controllers;
using ApiPlayground.services;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using CSharpPlayground.services;

namespace ApiPlayground.Startup;

public class AutofacConfiguration
{
    public static ContainerBuilder Configure(IServiceCollection serviceCollection)
    {
        var builder = new ContainerBuilder();
        // builder.RegisterType<ProgramController>().As<IProgramController>();
        builder.RegisterType<BidmasCalculator>().As<IBidmasCalculator>();
        builder.RegisterType<LeftToRightCalculator>().As<ILeftToRightCalculator>();
        builder.RegisterType<StringParsingService>().As<IStringParsingService>();
        builder.Populate(serviceCollection);
        return builder;
    }

}