﻿using Autofac;
using Core;
using Core.Setup.Enums;
using Donate;
using Orders;

namespace Worker
{
    public static class UsedModules
    {
        public static readonly IEnumerable<Module> List = new List<Module>()
        {
            new CoreModule(WebApiUserProviderType.ProvideServiceUser),
            new DonateModule(),
            new OrdersModule(),
        };
    }
}