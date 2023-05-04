using Autofac;
using Core.Setup.Autofac;

namespace Orders.Actions.AvailableDeliveriesActions.AvailableDeliveriesGetList
{
    public class AvailableDeliveriesGetListModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<AvailableDeliveriesGetListAction>();
            builder.RegisterAsScoped<AvailableDeliveriesGetListService>();
        }
    }
}