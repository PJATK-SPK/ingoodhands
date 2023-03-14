using Autofac;
using Core.Setup.Autofac;

namespace Orders.Actions.CreateOrderActions.CreateOrderAddAddress
{
    public class CreateOrderAddAddressModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<CreateOrderAddAddressAction>();
            builder.RegisterAsScoped<CreateOrderAddAddressService>();
            builder.RegisterAsScoped<CreateOrderAddAddressPayloadValidator>();
        }
    }
}