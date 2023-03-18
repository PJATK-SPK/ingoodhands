using Autofac;
using Core.Setup.Autofac;

namespace Orders.Jobs.CreateDeliveries
{
    public class CreateDeliveriesJobModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<CreateDeliveriesJob>();
            builder.RegisterAsScoped<CreateDeliveriesJobDataService>();
            builder.RegisterAsScoped<CreateDeliveriesJobOrderRemainderService>();
            builder.RegisterAsScoped<CreateDeliveriesJobWarehouseService>();
        }
    }
}
