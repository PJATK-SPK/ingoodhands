using Autofac;
using Core;
using Core.Database;
using Core.Database.Seeders;
using Core.Setup.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orders.Jobs.CreateDeliveries;
using Orders;
using TestsBase;

namespace OrderTests.Jobs.CreateDeliveries
{
    [TestClass()]
    public class CreateDeliveriesJobTest
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
             * Order 1 - cancelled
             * 
             * Order 2 na 30 mleka, 200 ryżu, 20 makaronu, 40 płatek
             * Bez delieverek
             * Warehouse 1: 100 mleka, 100 ryżu
             * Warehouse 2: 50 mleka, 50 makaronu
             * -- Efekt: 
             * Delivery 1 z Warehouse 1 - pobór 30 mleka, 100 ryżu
             * Delivery 2 z Warehouse 2 - pobór 20 makaronu
             * -- Stock:
             * Warehouse 1: 70 mleka
             * Warehouse 2: 50 mleka, 30 makaronu
             */
            await RunPart1OfCompleteTest(toolkit);

            var context = toolkit.Resolve<AppDbContext>();
            context.Deliveries.ToList().ForEach(c => c.IsDelivered = true);
            await context.SaveChangesAsync();

            /**
             * Pozostało 0 mleka, 100 ryżu, 0 makaronu, 40 płatek
             * 2 delivery
             * Warehouse 1: 70 mleka
             * Warehouse 2: 50 mleka, 30 makaronu (+300 ryżu)
             * Warehouse 3: (+50 płatek)
             * -- Efekt: 
             * Delivery 3 z Warehouse 2 - pobór 100 ryżu
             * Delivery 4 z Warehouse 3 - pobór 40 płatek
             * -- Stock:
             * Warehouse 1: 70 mleka
             * Warehouse 2: 50 mleka, 30 makaronu, 200 ryżu
             * Warehouse 3: 10 płatek
             */
            await RunPart2OfCompleteTest(toolkit);
        }

        private static async Task RunPart1OfCompleteTest(TestsToolkit toolkit)
        {
            var orders = CreateDeliveriesJobFixture.CreateOrdersForCompleteTest(toolkit);
            var stocks = CreateDeliveriesJobFixture.CreateStockForCompleteTestPart1();
            var users = CreateDeliveriesJobFixture.CreateUsersForCompleteTest();

            var context = toolkit.Resolve<AppDbContext>();
            var task1 = context.Users.AddRangeAsync(users);
            var task2 = context.Orders.AddRangeAsync(orders);
            var task3 = context.Stocks.AddRangeAsync(stocks);
            await Task.WhenAll(task1, task2, task3);
            await context.SaveChangesAsync();

            var job = toolkit.Resolve<CreateDeliveriesJob>();

            await job.Execute();

            var deliveries = await context.Deliveries.Include(c => c.DeliveryProducts).ToListAsync();
            var delivery1 = deliveries.Single(c => c.WarehouseId == WarehouseSeeder.Warehouse1PL.Id);
            var delivery2 = deliveries.Single(c => c.WarehouseId == WarehouseSeeder.Warehouse2PL.Id);

            // Check deliveries
            Assert.AreEqual(2, delivery1.DeliveryProducts!.Count);
            Assert.AreEqual(30, delivery1.DeliveryProducts!.Single(c => c.ProductId == ProductSeeder.Product9Milk.Id).Quantity);
            Assert.AreEqual(100, delivery1.DeliveryProducts!.Single(c => c.ProductId == ProductSeeder.Product1Rice.Id).Quantity);

            Assert.AreEqual(1, delivery2.DeliveryProducts!.Count);
            Assert.AreEqual(20, delivery2.DeliveryProducts!.Single(c => c.ProductId == ProductSeeder.Product2Pasta.Id).Quantity);

            // Check stocks
            var stockMilkWarehouse1 = stocks.Single(c => c.WarehouseId == WarehouseSeeder.Warehouse1PL.Id && c.ProductId == ProductSeeder.Product9Milk.Id);
            var stockRiceWarehouse1 = stocks.Single(c => c.WarehouseId == WarehouseSeeder.Warehouse1PL.Id && c.ProductId == ProductSeeder.Product1Rice.Id);
            var stockMilkWarehouse2 = stocks.Single(c => c.WarehouseId == WarehouseSeeder.Warehouse2PL.Id && c.ProductId == ProductSeeder.Product9Milk.Id);
            var stockPastaWarehouse2 = stocks.Single(c => c.WarehouseId == WarehouseSeeder.Warehouse2PL.Id && c.ProductId == ProductSeeder.Product2Pasta.Id);

            Assert.AreEqual(70, stockMilkWarehouse1.Quantity);
            Assert.AreEqual(0, stockRiceWarehouse1.Quantity);
            Assert.AreEqual(50, stockMilkWarehouse2.Quantity);
            Assert.AreEqual(30, stockPastaWarehouse2.Quantity);
        }

        private static async Task RunPart2OfCompleteTest(TestsToolkit toolkit)
        {
            var context = toolkit.Resolve<AppDbContext>();
            var order = await context.Orders.Where(c => !c.IsCanceledByUser).SingleAsync();
            var stocks = await context.Stocks.ToListAsync();

            Assert.AreEqual(2, order.Deliveries!.Count);

            var newStocks = CreateDeliveriesJobFixture.CreateStockForCompleteTestPart2();
            await context.Stocks.AddRangeAsync(newStocks);
            await context.SaveChangesAsync();
            stocks = await context.Stocks.ToListAsync();

            var job = toolkit.Resolve<CreateDeliveriesJob>();

            await job.Execute();

            var deliveries = await context.Deliveries.Where(c => !c.IsDelivered).Include(c => c.DeliveryProducts).ToListAsync();
            var delivery3 = deliveries.Single(c => c.WarehouseId == WarehouseSeeder.Warehouse2PL.Id);
            var delivery4 = deliveries.Single(c => c.WarehouseId == WarehouseSeeder.Warehouse3PL.Id);

            // Check deliveries
            Assert.AreEqual(1, delivery3.DeliveryProducts!.Count);
            Assert.AreEqual(100, delivery3.DeliveryProducts!.Single(c => c.ProductId == ProductSeeder.Product1Rice.Id).Quantity);

            Assert.AreEqual(1, delivery4.DeliveryProducts!.Count);
            Assert.AreEqual(40, delivery4.DeliveryProducts!.Single(c => c.ProductId == ProductSeeder.Product3Cereals.Id).Quantity);

            // Check stocks
            var stockMilkWarehouse1 = stocks.Single(c => c.WarehouseId == WarehouseSeeder.Warehouse1PL.Id && c.ProductId == ProductSeeder.Product9Milk.Id);
            var stockRiceWarehouse1 = stocks.Single(c => c.WarehouseId == WarehouseSeeder.Warehouse1PL.Id && c.ProductId == ProductSeeder.Product1Rice.Id);
            var stockMilkWarehouse2 = stocks.Single(c => c.WarehouseId == WarehouseSeeder.Warehouse2PL.Id && c.ProductId == ProductSeeder.Product9Milk.Id);
            var stockPastaWarehouse2 = stocks.Single(c => c.WarehouseId == WarehouseSeeder.Warehouse2PL.Id && c.ProductId == ProductSeeder.Product2Pasta.Id);
            var stockRiceWarehouse2 = stocks.Single(c => c.WarehouseId == WarehouseSeeder.Warehouse2PL.Id && c.ProductId == ProductSeeder.Product1Rice.Id);
            var stockCerealsWarehouse3 = stocks.Single(c => c.WarehouseId == WarehouseSeeder.Warehouse3PL.Id && c.ProductId == ProductSeeder.Product3Cereals.Id);

            Assert.AreEqual(70, stockMilkWarehouse1.Quantity);
            Assert.AreEqual(0, stockRiceWarehouse1.Quantity);
            Assert.AreEqual(50, stockMilkWarehouse2.Quantity);
            Assert.AreEqual(30, stockPastaWarehouse2.Quantity);
            Assert.AreEqual(200, stockRiceWarehouse2.Quantity);
            Assert.AreEqual(10, stockCerealsWarehouse3.Quantity);
        }
    }
}
