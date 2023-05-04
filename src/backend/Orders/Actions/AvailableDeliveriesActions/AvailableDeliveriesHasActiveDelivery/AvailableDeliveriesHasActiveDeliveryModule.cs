using Autofac;
using Core.Setup.Autofac;

namespace Orders.Actions.AvailableDeliveriesActions.AvailableDeliveriesHasActiveDelivery
{
    public class AvailableDeliveriesHasActiveDeliveryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<AvailableDeliveriesHasActiveDeliveryAction>();
            builder.RegisterAsScoped<AvailableDeliveriesHasActiveDeliveryService>();
        }
    }
}