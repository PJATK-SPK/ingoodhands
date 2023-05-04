using Autofac;
using Core.Setup.Autofac;

namespace Orders.Actions.DeliveriesActions.DeliveriesGetWarehouseDeliveriesCount
{
    public class DeliveriesGetWarehouseDeliveriesCountModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<DeliveriesGetWarehouseDeliveriesCountAction>();
            builder.RegisterAsScoped<DeliveriesGetWarehouseDeliveriesCountService>();
        }
    }
}