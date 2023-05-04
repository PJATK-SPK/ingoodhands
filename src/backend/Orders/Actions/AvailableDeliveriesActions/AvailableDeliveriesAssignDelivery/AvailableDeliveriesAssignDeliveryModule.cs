using Autofac;
using Core.Setup.Autofac;

namespace Orders.Actions.AvailableDeliveriesActions.AvailableDeliveriesAssignDelivery
{
    public class AvailableDeliveriesAssignDeliveryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<AvailableDeliveriesAssignDeliveryAction>();
            builder.RegisterAsScoped<AvailableDeliveriesAssignDeliveryService>();
        }
    }
}