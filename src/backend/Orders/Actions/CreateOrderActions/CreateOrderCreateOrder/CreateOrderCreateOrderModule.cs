using Autofac;
using Core.Setup.Autofac;

namespace Orders.Actions.CreateOrderActions.CreateOrderCreateOrder
{
    public class CreateOrderCreateOrderModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<CreateOrderCreateOrderAction>();
            builder.RegisterAsScoped<CreateOrderCreateOrderService>();
            builder.RegisterAsScoped<CreateOrderCreateOrderPayloadValidator>();
        }
    }
}