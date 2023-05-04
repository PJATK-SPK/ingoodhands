﻿using Autofac;
using Core.Setup.Autofac;
using Orders.Actions.AvailableDeliveriesActions.AvailableDeliveriesCount;
using Orders.Actions.CreateOrderActions.CreateOrderAddAddress;
using Orders.Actions.CreateOrderActions.CreateOrderCreateOrder;
using Orders.Actions.CreateOrderActions.CreateOrderDeleteAddress;
using Orders.Actions.CreateOrderActions.CreateOrderGetAddresses;
using Orders.Actions.CreateOrderActions.CreateOrderGetCountries;
using Orders.Actions.CurrentDelivery.CurrentDeliveryGetSingle;
using Orders.Actions.DeliveriesActions.DeliveriesGetList;
using Orders.Actions.DeliveriesActions.DeliveriesGetSingle;
using Orders.Actions.DeliveriesActions.DeliveriesPickup;
using Orders.Actions.DeliveriesActions.DeliveriesSetLost;
using Orders.Actions.OrdersActions.OrdersCancel;
using Orders.Actions.OrdersActions.OrdersGetSingle;
using Orders.Actions.OrdersActions.OrdersSetAsDelivered;
using Orders.Actions.RequestHelpActions.RequestHelpGetMap;
using Orders.Actions.StocksActions.StocksGetList;
using Orders.Actions.WarehousesActions.GetWarehousesList;
using Orders.Jobs.CreateDeliveries;
using Orders.Jobs.RecalcOrdersPercentage;
using Orders.Services.DeliveryNameBuilder;
using Orders.Services.OrderNameBuilder;

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
            builder.RegisterModule<OrdersCancelModule>();
            builder.RegisterModule<StocksGetListModule>();
            builder.RegisterModule<OrdersGetSingleModule>();
            builder.RegisterModule<DeliveriesPickupModule>();
            builder.RegisterModule<DeliveriesGetListModule>();
            builder.RegisterModule<WarehousesGetListModule>();
            builder.RegisterModule<RequestHelpGetMapModule>();
            builder.RegisterModule<DeliveriesSetLostModule>();
            builder.RegisterModule<DeliveriesGetSingleModule>();
            builder.RegisterModule<OrdersSetAsDeliveredModule>();
            builder.RegisterModule<CreateOrderAddAddressModule>();
            builder.RegisterModule<CreateOrderCreateOrderModule>();
            builder.RegisterModule<CreateOrderGetCountriesModule>();
            builder.RegisterModule<CreateOrderGetAddressesModule>();
            builder.RegisterModule<CurrentDeliveryGetSingleModule>();
            builder.RegisterModule<AvailableDeliveriesCountModule>();
            builder.RegisterModule<CreateOrderDeleteAddressModule>();
        }

        private static void RegisterJobs(ContainerBuilder builder)
        {
            builder.RegisterModule<CreateDeliveriesJobModule>();
            builder.RegisterModule<RecalcOrdersPercentageJobModule>();
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<OrderNameBuilderService>();
            builder.RegisterAsScoped<DeliveryNameBuilderService>();
        }
    }
}
