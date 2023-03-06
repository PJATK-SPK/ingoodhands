using Autofac;
using Core.Setup.Autofac;

namespace Order.Actions.WarehousesActions.GetWarehousesList
{
    public class WarehousesGetListModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<WarehousesGetListAction>();
        }
    }
}
