using Autofac;
using Core.Setup.Autofac;

namespace Orders.Actions.DeliveriesActions.DeliveriesSetLost
{
    public class DeliveriesSetLostModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<DeliveriesSetLostAction>();
        }
    }
}