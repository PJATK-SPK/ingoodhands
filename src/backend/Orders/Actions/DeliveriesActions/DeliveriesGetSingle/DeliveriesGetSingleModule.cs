using Autofac;
using Core.Setup.Autofac;

namespace Orders.Actions.DeliveriesActions.DeliveriesGetSingle
{
    public class DeliveriesGetSingleModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<DeliveriesGetSingleAction>();
            builder.RegisterAsScoped<DeliveriesGetSingleService>();
        }
    }
}