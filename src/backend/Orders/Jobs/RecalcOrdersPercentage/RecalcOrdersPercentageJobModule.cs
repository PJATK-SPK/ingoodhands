using Autofac;
using Core.Setup.Autofac;
using Orders.Jobs.CreateDeliveries;

namespace Orders.Jobs.RecalcOrdersPercentage
{
    public class RecalcOrdersPercentageJobModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<RecalcOrdersPercentageJob>();
            builder.RegisterAsScoped<RecalcOrdersPercentageJobDataService>();
            builder.RegisterAsScoped<RecalcOrdersPercentageJobService>();
        }
    }
}
