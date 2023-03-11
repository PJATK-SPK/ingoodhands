using Autofac;
using Core.Setup.Autofac;
using Orders.Actions.StocksActions.StocksGetList;
using Orders.Actions.WarehousesActions.GetWarehousesList;
using Orders.Services.OrderNameBuilder;
using Orders.Services.DeliveryNameBuilder;
using Orders.Actions.RequestHelpActions.RequestHelpGetMap;
using Orders.Actions.CreateOrderActions.CreateOrderGetCountries;

namespace Orders
{
    public class OrdersModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            RegisterActions(builder);
            RegisterJobs(builder);
            RegisterServices(builder);
        }

        private static void RegisterActions(ContainerBuilder builder)
        {
            builder.RegisterModule<StocksGetListModule>();
            builder.RegisterModule<WarehousesGetListModule>();
            builder.RegisterModule<RequestHelpGetMapModule>();
            builder.RegisterModule<CreateOrderGetCountriesModule>();
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
