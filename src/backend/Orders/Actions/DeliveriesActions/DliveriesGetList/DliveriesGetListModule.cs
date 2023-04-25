using Autofac;
using Core.Setup.Autofac;

namespace Orders.Actions.DeliveriesActions.DliveriesGetList
{
    public class DliveriesGetListModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<DliveriesGetListAction>();
            builder.RegisterAsScoped<DliveriesGetListService>();
        }
    }
}