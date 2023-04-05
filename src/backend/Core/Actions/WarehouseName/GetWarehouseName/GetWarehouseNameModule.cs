using Autofac;
using Core.Actions.MyNotifications.UpdateWebPush;
using Core.Setup.Autofac;

namespace Core.Actions.WarehouseName.GetWarehouseName
{
    public class GetWarehouseNameModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<GetWarehouseNameAction>();
            builder.RegisterAsScoped<GetWarehouseNameService>();
        }
    }
}
