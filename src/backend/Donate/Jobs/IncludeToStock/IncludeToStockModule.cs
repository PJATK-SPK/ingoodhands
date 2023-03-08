using Autofac;
using Core.Setup.Autofac;

namespace Donate.Jobs.IncludeToStock
{
    public class IncludeToStockModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<IncludeToStockJob>();
        }
    }
}