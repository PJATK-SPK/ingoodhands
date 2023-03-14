using Autofac;
using Core.Setup.Autofac;

namespace Orders.Actions.CreateOrderActions.CreateOrderDeleteAddress
{
    public class CreateOrderDeleteAddressModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<CreateOrderDeleteAddressAction>();
            builder.RegisterAsScoped<CreateOrderDeleteAddressService>();
        }
    }
}