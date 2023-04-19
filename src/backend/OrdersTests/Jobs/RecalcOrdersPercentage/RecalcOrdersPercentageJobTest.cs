using Autofac;
using Core;
using Core.Database;
using Core.Database.Enums;
using Core.Database.Seeders;
using Core.Setup.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orders;
using Orders.Jobs.RecalcOrdersPercentage;
using Orders.Services.OrderNameBuilder;
using TestsBase;

namespace OrdersTests.Jobs.RecalcOrdersPercentage
{
    [TestClass()]
    public class RecalcOrdersPercentageJobTest
    {
        private readonly List<Module> _usedModules = new()
        {
            new CoreModule(WebApiUserProviderType.None),
            new OrdersModule(),
        };

        [TestMethod()]
        public async Task Complete_test_from_start_to_end()
        {
            using var toolkit = new TestsToolkit(_usedModules);

            /**
            * Order 1 - cancelled, warehouse1
            * Order 2 na 200 mleka, 200 ryżu, warehouse4
            * Order 3 na 100 pasta, warehouse2
            * Order 4 na 350 mleka, 50 ryżu, 150 energyDrink, 50 Walnuts warehouse3
            * 
            * Delivery 1 - ORDER 2 - dostawa 100 mleka, 100 ryżu
            * Delivery 2 - ORDER 3 - dostawa 100 pasta, 
            * Delivery 3 - ORDER 4 - dostawa 50 mleka, 50 ryzu, 50 energola, 50 walnutuf
            * 
            * -- Efekt: 
            * Order 2 = 50% Complete
            * Order 3 = 100% Complete - marked as Status-Inactive
            * Order 4 = 33% Complete 
            */


            await RunPart1OfCompleteTest(toolkit);

            var context = toolkit.Resolve<AppDbContext>();
            await context.SaveChangesAsync();

            /**
             * Order 1 - cancelled
             * Pozostało Order2: 100 mleka, 100 ryżu
             * Pozostało Order3: Completed 100%
             * Pozostało Order4: 300 mleka, 0 ryżu, 100 energyDrink, 0 Walnuts
             * 
             * * Delivery 1 - ORDER 2 - 100 ryżu - NOT DELIVERED ( nie brane pod uwagę )
             * * Delivery 2 - ORDER 4 - dostawa 50 mleka, 50 energyDrink
             * 
             * -- Efekt: 
             * Order 2 = 50% Complete
             * Order 3 = 100% Complete - marked as Status-Inactive
             * Order 4 = 50% Complete
             */

            await RunPart2OfCompleteTest(toolkit);
            await context.SaveChangesAsync();
            /**
             * Order 1 - cancelled
             * Pozostało Order2: 100 mleka, 100 ryżu
             * Pozostało Order3: Completed 100%
             * Pozostało Order4: 250 mleka, 0 ryżu, 50 energyDrink, 0 Walnuts
             * 
             * * Delivery 1 - ORDER 2 - dostawa 100 mleka
             * * Delivery 2 - ORDER 4 - dostawa 250 mleka, 50 energyDrink
             * 
             * -- Efekt: 
             * Order 2 = 100% Complete - marked as Status-Inactive
             * Order 3 = 100% Complete - marked as Status-Inactive
             * Order 4 = 100% Complete - marked as Status-Inactive
             */

            await RunPart3OfCompleteTest(toolkit);
            await context.SaveChangesAsync();
        }

        private static async Task RunPart1OfCompleteTest(TestsToolkit toolkit)
        {
            var orders = RecalcOrdersPercentageJobFixture.CreateOrdersForCompleteTest(toolkit);
            var users = RecalcOrdersPercentageJobFixture.CreateUsersForCompleteTest();

            var context = toolkit.Resolve<AppDbContext>();
            var task1 = context.Users.AddRangeAsync(users);
            var task2 = context.Orders.AddRangeAsync(orders);
            await Task.WhenAll(task1, task2);

            var orderNameBuilder = toolkit.Resolve<OrderNameBuilderService>();

            var user1 = users.Single(c => c.FirstName == "Jan1");
            var user2 = users.Single(c => c.FirstName == "Jan2");

            var order1 = orders.Single(c => c.IsCanceledByUser);
            var order2 = orders.Single(c => c.Name == orderNameBuilder.Build(2));
            var order3 = orders.Single(c => c.Name == orderNameBuilder.Build(3));
            var order4 = orders.Single(c => c.Name == orderNameBuilder.Build(4));

            var deliveries = RecalcOrdersPercentageJobFixture.CreateDeliveryForCompleteTestPart1(toolkit, order2, order3, order4, user1, user2);

            var delivery1 = deliveries.Single(c => c.WarehouseId == WarehouseSeeder.Warehouse4DE.Id);
            var delivery2 = deliveries.Single(c => c.WarehouseId == WarehouseSeeder.Warehouse2PL.Id);
            var delivery3 = deliveries.Single(c => c.WarehouseId == WarehouseSeeder.Warehouse3PL.Id);

            order2.Deliveries!.Add(delivery1);
            order3.Deliveries!.Add(delivery2);
            order4.Deliveries!.Add(delivery3);

            await context.SaveChangesAsync();

            var job = toolkit.Resolve<RecalcOrdersPercentageJob>();

            await job.Execute();

            // Check deliveries
            Assert.AreEqual(2, delivery1.DeliveryProducts!.Count);
            Assert.AreEqual(100, delivery1.DeliveryProducts!.Single(c => c.ProductId == ProductSeeder.Product9Milk.Id).Quantity);
            Assert.AreEqual(100, delivery1.DeliveryProducts!.Single(c => c.ProductId == ProductSeeder.Product1Rice.Id).Quantity);

            Assert.AreEqual(1, delivery2.DeliveryProducts!.Count);
            Assert.AreEqual(100, delivery2.DeliveryProducts!.Single(c => c.ProductId == ProductSeeder.Product2Pasta.Id).Quantity);

            Assert.AreEqual(4, delivery3.DeliveryProducts!.Count);
            Assert.AreEqual(50, delivery3.DeliveryProducts!.Single(c => c.ProductId == ProductSeeder.Product9Milk.Id).Quantity);
            Assert.AreEqual(50, delivery3.DeliveryProducts!.Single(c => c.ProductId == ProductSeeder.Product1Rice.Id).Quantity);
            Assert.AreEqual(50, delivery3.DeliveryProducts!.Single(c => c.ProductId == ProductSeeder.Product12EnergyDrink.Id).Quantity);
            Assert.AreEqual(50, delivery3.DeliveryProducts!.Single(c => c.ProductId == ProductSeeder.Product5Walnuts.Id).Quantity);

            // Check orders
            Assert.AreEqual(true, order1.IsCanceledByUser);
            Assert.AreEqual(0, order1.Percentage);
            Assert.AreEqual(50, order2.Percentage);
            Assert.AreEqual(100, order3.Percentage);
            Assert.AreEqual(33, order4.Percentage);
        }

        private static async Task RunPart2OfCompleteTest(TestsToolkit toolkit)
        {
            var context = toolkit.Resolve<AppDbContext>();
            var orders = await context.Orders.ToListAsync();
            var users = await context.Users.ToListAsync();

            var orderNameBuilder = toolkit.Resolve<OrderNameBuilderService>();

            var user1 = users.Single(c => c.FirstName == "Jan1");
            var user2 = users.Single(c => c.FirstName == "Jan2");

            var order1 = orders.Single(c => c.IsCanceledByUser);
            var order2 = orders.Single(c => c.Name == orderNameBuilder.Build(2));
            var order3 = orders.Single(c => c.Name == orderNameBuilder.Build(3));
            var order4 = orders.Single(c => c.Name == orderNameBuilder.Build(4));

            var deliveries = RecalcOrdersPercentageJobFixture.CreateDeliveryForCompleteTestPart2(toolkit, order2, order4, user1, user2);

            var delivery1 = deliveries.Single(c => c.WarehouseId == WarehouseSeeder.Warehouse4DE.Id);
            var delivery2 = deliveries.Single(c => c.WarehouseId == WarehouseSeeder.Warehouse3PL.Id);

            order2.Deliveries!.Add(delivery1);
            order4.Deliveries!.Add(delivery2);

            await context.SaveChangesAsync();

            var job = toolkit.Resolve<RecalcOrdersPercentageJob>();

            await job.Execute();

            // Check deliveries
            Assert.AreEqual(1, delivery1.DeliveryProducts!.Count);
            Assert.AreEqual(100, delivery1.DeliveryProducts!.Single(c => c.ProductId == ProductSeeder.Product1Rice.Id).Quantity);

            Assert.AreEqual(2, delivery2.DeliveryProducts!.Count);
            Assert.AreEqual(50, delivery2.DeliveryProducts!.Single(c => c.ProductId == ProductSeeder.Product9Milk.Id).Quantity);
            Assert.AreEqual(50, delivery2.DeliveryProducts!.Single(c => c.ProductId == ProductSeeder.Product12EnergyDrink.Id).Quantity);

            // Check orders
            Assert.AreEqual(true, order1.IsCanceledByUser);
            Assert.AreEqual(0, order1.Percentage);
            Assert.AreEqual(50, order2.Percentage);
            Assert.AreEqual(100, order3.Percentage);
            Assert.AreEqual(DbEntityStatus.Inactive, order3.Status);
            Assert.AreEqual(50, order4.Percentage);
        }

        private static async Task RunPart3OfCompleteTest(TestsToolkit toolkit)
        {
            var context = toolkit.Resolve<AppDbContext>();
            var orders = await context.Orders.ToListAsync();
            var users = await context.Users.ToListAsync();

            var orderNameBuilder = toolkit.Resolve<OrderNameBuilderService>();

            var user1 = users.Single(c => c.FirstName == "Jan1");
            var user2 = users.Single(c => c.FirstName == "Jan2");

            var order1 = orders.Single(c => c.IsCanceledByUser);
            var order2 = orders.Single(c => c.Name == orderNameBuilder.Build(2));
            var order3 = orders.Single(c => c.Name == orderNameBuilder.Build(3));
            var order4 = orders.Single(c => c.Name == orderNameBuilder.Build(4));

            var deliveries = RecalcOrdersPercentageJobFixture.CreateDeliveryForCompleteTestPart3(toolkit, order2, order4, user1, user2);

            var delivery1 = deliveries.Single(c => c.WarehouseId == WarehouseSeeder.Warehouse4DE.Id);
            var delivery2 = deliveries.Single(c => c.WarehouseId == WarehouseSeeder.Warehouse3PL.Id);

            order2.Deliveries!.Add(delivery1);
            order4.Deliveries!.Add(delivery2);

            await context.SaveChangesAsync();

            var job = toolkit.Resolve<RecalcOrdersPercentageJob>();

            await job.Execute();

            // Check deliveries
            Assert.AreEqual(2, delivery1.DeliveryProducts!.Count);
            Assert.AreEqual(100, delivery1.DeliveryProducts!.Single(c => c.ProductId == ProductSeeder.Product9Milk.Id).Quantity);
            Assert.AreEqual(100, delivery1.DeliveryProducts!.Single(c => c.ProductId == ProductSeeder.Product1Rice.Id).Quantity);

            Assert.AreEqual(2, delivery2.DeliveryProducts!.Count);
            Assert.AreEqual(250, delivery2.DeliveryProducts!.Single(c => c.ProductId == ProductSeeder.Product9Milk.Id).Quantity);
            Assert.AreEqual(50, delivery2.DeliveryProducts!.Single(c => c.ProductId == ProductSeeder.Product12EnergyDrink.Id).Quantity);

            // Check orders
            Assert.AreEqual(true, order1.IsCanceledByUser);
            Assert.AreEqual(0, order1.Percentage);
            Assert.AreEqual(100, order2.Percentage);
            Assert.AreEqual(DbEntityStatus.Inactive, order2.Status);
            Assert.AreEqual(100, order3.Percentage);
            Assert.AreEqual(DbEntityStatus.Inactive, order3.Status);
            Assert.AreEqual(100, order4.Percentage);
            Assert.AreEqual(DbEntityStatus.Inactive, order4.Status);
        }
    }
}
