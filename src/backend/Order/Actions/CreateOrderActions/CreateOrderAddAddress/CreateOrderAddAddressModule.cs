using Autofac;
using Core.Setup.Autofac;
using Orders.Actions.CreateOrderActions.CreateOrderAddAddresses;

namespace Orders.Actions.CreateOrderActions.CreateOrderAddAddress
{
    public class CreateOrderAddAddressModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<CreateOrderAddAddressAction>();
            builder.RegisterAsScoped<CreateOrderAddAddressService>();
        }
    }
}