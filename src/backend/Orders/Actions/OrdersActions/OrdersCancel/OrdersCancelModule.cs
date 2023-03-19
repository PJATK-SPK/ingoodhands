using Autofac;
using Core.Setup.Autofac;

namespace Orders.Actions.OrdersActions.OrdersCancel
{
    public class OrdersCancelModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<OrdersCancelAction>();
            builder.RegisterAsScoped<OrdersCancelService>();
        }
    }
}