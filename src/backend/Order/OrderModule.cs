﻿using Autofac;
using Core.Setup.Autofac;
using Order.Actions.WarehousesActions.GetWarehousesList;

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
        }

        private static void RegisterJobs(ContainerBuilder builder)
        {
            // Will be used in future
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            // Will be used in future
        }
    }
}
