using Autofac;
using Core.Actions.DonateForm.GetProducts;
using Core.Actions.MyNotifications.GetList;
using Core.Actions.MyNotifications.UpdateWebPush;
using Core.Actions.WarehouseName.GetWarehouseName;
using Core.Services;
using Core.Setup;
using Core.Setup.Autofac;
using Core.Setup.Enums;

namespace Core
{
    public class CoreModule : Module
    {
        private readonly SetupModule _setupModule;

        public CoreModule(WebApiUserProviderType userProviderType)
        {
            _setupModule = new SetupModule(userProviderType);
        }

        protected override void Load(ContainerBuilder builder)
        {
            _setupModule.RegisterAll(builder);
            RegisterActions(builder);
            RegisterServices(builder);
        }

        private static void RegisterActions(ContainerBuilder builder)
        {
            builder.RegisterModule<GetProductsModule>();
            builder.RegisterModule<GetWarehouseNameModule>();
            builder.RegisterModule<MyNotificationsUpdateWebPushModule>();
            builder.RegisterModule<MyNotificationsGetListLast30DaysModule>();
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<RoleService>();
            builder.RegisterAsScoped<CounterService>();
            builder.RegisterAsScoped<NotificationService>();
            builder.RegisterAsScoped<GetCurrentUserService>();
        }
    }
}
