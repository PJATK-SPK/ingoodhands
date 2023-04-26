using Autofac;
using Core.Setup.Autofac;

namespace Orders.Actions.DeliveriesActions.DeliveriesPickup
{
    public class DeliveriesPickupModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<DeliveriesPickupAction>();
        }
    }
}