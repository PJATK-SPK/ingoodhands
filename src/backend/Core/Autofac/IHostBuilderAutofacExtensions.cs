using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Core.Autofac
{
    public static class IHostBuilderAutofacExtensions
    {
        public static void SetupAutofac(this IHostBuilder host, IEnumerable<Module> modules)
        {
            host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            host.ConfigureContainer<ContainerBuilder>(builder =>
            {
                foreach (var module in modules)
                    builder.RegisterModule(module);
            });
        }
    }
}
