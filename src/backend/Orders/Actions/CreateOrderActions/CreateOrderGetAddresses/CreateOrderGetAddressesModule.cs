using Autofac;
using Core.Setup.Autofac;

namespace Orders.Actions.CreateOrderActions.CreateOrderGetAddresses
{
    public class CreateOrderGetAddressesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<CreateOrderGetAddressesAction>();
            builder.RegisterAsScoped<CreateOrderGetAddressesService>();
        }
    }
}