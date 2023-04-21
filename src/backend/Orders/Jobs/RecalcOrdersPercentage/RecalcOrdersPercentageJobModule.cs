using Autofac;
using Core.Setup.Autofac;

namespace Orders.Jobs.RecalcOrdersPercentage
{
    public class RecalcOrdersPercentageJobModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<RecalcOrdersPercentageJob>();
            builder.RegisterAsScoped<RecalcOrdersPercentageJobDataService>();
        }
    }
}
