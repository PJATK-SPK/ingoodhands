using Autofac;
using Core.Setup.Autofac;

namespace Orders.Actions.CurrentDelivery.CurrentDeliveryGetSingle
{
    public class CurrentDeliveryGetSingleModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<CurrentDeliveryGetSingleAction>();
            builder.RegisterAsScoped<CurrentDeliveryGetSingleService>();
        }
    }
}