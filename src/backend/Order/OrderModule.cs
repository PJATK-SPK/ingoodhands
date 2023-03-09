using Autofac;
using Core.Setup.Autofac;
using Order.Actions.StocksActions.StocksGetList;
using Order.Actions.WarehousesActions.GetWarehousesList;
using Order.Services.OrderNameBuilder;
using Order.Services.DeliveryNameBuilder;

namespace Order
{
    public class OrderModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            RegisterActions(builder);
            RegisterJobs(builder);
            RegisterServices(builder);
        }

        private static void RegisterActions(ContainerBuilder builder)
        {
            builder.RegisterModule<WarehousesGetListModule>();
            builder.RegisterModule<StocksGetListModule>();
        }

        private static void RegisterJobs(ContainerBuilder builder)
        {
            // Will be used in future
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<OrderNameBuilderService>();
            builder.RegisterAsScoped<DeliveryNameBuilderService>();
        }
    }
}
