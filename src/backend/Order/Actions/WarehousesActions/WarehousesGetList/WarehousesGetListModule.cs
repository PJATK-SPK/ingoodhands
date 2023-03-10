using Autofac;
using Core.Setup.Autofac;

namespace Orders.Actions.WarehousesActions.GetWarehousesList
{
    public class WarehousesGetListModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<WarehousesGetListAction>();
        }
    }
}
