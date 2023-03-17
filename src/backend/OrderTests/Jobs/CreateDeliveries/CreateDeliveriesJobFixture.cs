using Core.Database.Models.Core;
using Core.Database.Seeders;
using Orders.Services.OrderNameBuilder;
using TestsBase;

namespace OrderTests.Jobs.CreateDeliveries
{
    public static class CreateDeliveriesJobFixture
    {
        public static List<Order> CreateOrdersForCompleteTest(TestsToolkit toolkit)
        {
            var builder = toolkit.Resolve<OrderNameBuilderService>();

            var order1 = new Order
            {
                AddressId = WarehouseSeeder.Warehouse1PL.Id,
                Name = builder.Build(1),
                Percentage = 0,
                OwnerUserId = UserSeeder.ServiceUser.Id,
                CreationDate = DateTime.UtcNow,
                IsCanceledByUser = true,
                OrderProducts = new List<OrderProduct>
                {
                    new OrderProduct
                    {
                        ProductId = ProductSeeder.Product5Walnuts.Id,
                        Quantity = 1
                    },
                }
            };

            var order2 = new Order
            {
                AddressId = WarehouseSeeder.Warehouse1PL.Id,
                Name = builder.Build(2),
                Percentage = 0,
                OwnerUserId = UserSeeder.ServiceUser.Id,
                CreationDate = DateTime.UtcNow,
                IsCanceledByUser = false,
                OrderProducts = new List<OrderProduct>
                {
                    new OrderProduct
                    {
                        ProductId = ProductSeeder.Product9Milk.Id,
                        Quantity = 30
                    },
                    new OrderProduct
                    {
                        ProductId = ProductSeeder.Product1Rice.Id,
                        Quantity = 200
                    },
                    new OrderProduct
                    {
                        ProductId = ProductSeeder.Product2Pasta.Id,
                        Quantity = 20
                    },
                    new OrderProduct
                    {
                        ProductId = ProductSeeder.Product3Cereals.Id,
                        Quantity = 40
                    }
                }
            };

            return new List<Order> { order1, order2 };
        }

        public static List<Stock> CreateStockForCompleteTestPart1()
        {
            var w1Milk = new Stock
            {
                WarehouseId = WarehouseSeeder.Warehouse1PL.Id,
                ProductId = ProductSeeder.Product9Milk.Id,
                Quantity = 100,
            };

            var w1Rice = new Stock
            {
                WarehouseId = WarehouseSeeder.Warehouse1PL.Id,
                ProductId = ProductSeeder.Product1Rice.Id,
                Quantity = 100,
            };

            var w2Milk = new Stock
            {
                WarehouseId = WarehouseSeeder.Warehouse2PL.Id,
                ProductId = ProductSeeder.Product9Milk.Id,
                Quantity = 50,
            };

            var w2Pasta = new Stock
            {
                WarehouseId = WarehouseSeeder.Warehouse2PL.Id,
                ProductId = ProductSeeder.Product2Pasta.Id,
                Quantity = 50,
            };

            return new List<Stock> { w1Milk, w1Rice, w2Milk, w2Pasta };
        }

        public static List<Stock> CreateStockForCompleteTestPart2()
        {
            var w2Rice = new Stock
            {
                WarehouseId = WarehouseSeeder.Warehouse2PL.Id,
                ProductId = ProductSeeder.Product1Rice.Id,
                Quantity = 300,
            };

            var w3Cereals = new Stock
            {
                WarehouseId = WarehouseSeeder.Warehouse3PL.Id,
                ProductId = ProductSeeder.Product3Cereals.Id,
                Quantity = 50,
            };

            return new List<Stock> { w2Rice, w3Cereals };
        }
    }
}