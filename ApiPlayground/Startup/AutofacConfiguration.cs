using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Serilog;

namespace ApiPlayground.Startup
{
    public class AutofacConfiguration
    {
        public static ContainerBuilder Configure(IServiceCollection serviceCollection)
        {
            var builder = new ContainerBuilder();
            builder.RegisterInstance(Log.Logger).AsImplementedInterfaces();

            var endpointAssembly = Assembly.GetExecutingAssembly();
            GetImplementationInNamespace(endpointAssembly, "Services").ForEach(x =>
            {
                builder.RegisterType(x).AsImplementedInterfaces();
            });

            GetImplementationInNamespace(endpointAssembly, "Validators").ForEach(x =>
            {
                builder.RegisterType(x).AsImplementedInterfaces();
            });

            builder.Populate(serviceCollection);
            return builder;
        }

        private static List<Type> GetImplementationInNamespace(Assembly assembly, string namespacePartial)
        {
            return assembly.GetTypes()
                .Where(x => x.IsClass && !x.IsAbstract)
                .Where(x => !x.Name.ToLower().Contains("anonymous"))
                .Where(x => x.Namespace.EndsWith(namespacePartial))
                .ToList();
        }
    }
}