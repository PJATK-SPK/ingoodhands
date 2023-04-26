using Autofac;
using Core.Setup.Autofac;

namespace Orders.Actions.DeliveriesActions.DeliveriesGetList
{
    public class DeliveriesGetListModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<DeliveriesGetListAction>();
            builder.RegisterAsScoped<DeliveriesGetListService>();
        }
    }
}