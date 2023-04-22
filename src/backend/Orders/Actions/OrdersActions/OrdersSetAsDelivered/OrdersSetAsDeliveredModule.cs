using Autofac;
using Core.Setup.Autofac;

namespace Orders.Actions.OrdersActions.OrdersSetAsDelivered
{
    public class OrdersSetAsDeliveredModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<OrdersSetAsDeliveredAction>();
            builder.RegisterAsScoped<OrdersSetAsDeliveredService>();
        }
    }
}