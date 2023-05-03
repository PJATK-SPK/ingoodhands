using Autofac;
using Core.Setup.Autofac;

namespace Orders.Actions.AvailableDeliveriesActions.AvailableDeliveriesCount
{
    public class AvailableDeliveriesCountModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<AvailableDeliveriesCountAction>();
            builder.RegisterAsScoped<AvailableDeliveriesCountService>();
        }
    }
}