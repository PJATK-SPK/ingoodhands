using Autofac;
using Core.Setup.Autofac;

namespace Orders.Actions.OrdersActions.OrdersGetSingle
{
    public class OrdersGetSingleModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<OrdersGetSingleAction>();
            builder.RegisterAsScoped<OrdersGetSingleService>();
        }
    }
}