using Core.Database.Enums;
using Core.Database.Models.Auth;
using Core.Database.Models.Core;
using Core.Database.Seeders;
using Orders.Services.OrderNameBuilder;
using TestsBase;

namespace OrdersTests.Jobs.CreateDeliveries
{
    public static class CreateDeliveriesJobFixture
    {
        public static List<User> CreateUsersForCompleteTest()
        {
            var user1 = new User
            {
                Status = DbEntityStatus.Active,
                FirstName = "Jan1",
                LastName = "Kowalski",
                Email = "jan1.kowalski@ingoodhands.com",
                WarehouseId = WarehouseSeeder.Warehouse1PL.Id,
            };

            var user2 = new User
            {
                Status = DbEntityStatus.Active,
                FirstName = "Jan2",
                LastName = "Kowalski",
                Email = "jan2.kowalski@ingoodhands.com",
                WarehouseId = WarehouseSeeder.Warehouse2PL.Id
            };

            var user3 = new User
            {
                Status = DbEntityStatus.Active,
                FirstName = "Jan3",
                LastName = "Kowalski",
                Email = "jan3.kowalski@ingoodhands.com",
                WarehouseId = WarehouseSeeder.Warehouse3PL.Id
            };

            user1.Roles = new List<UserRole> { new UserRole() {
                User = user1,
                RoleId = RoleSeeder.Role5Deliverer.Id,
                UpdateUserId = UserSeeder.ServiceUser.Id,
                UpdatedAt = DateTime.UtcNow,
            } };
            user2.Roles = new List<UserRole> { new UserRole() {
                User = user2,
                RoleId = RoleSeeder.Role5Deliverer.Id,
                UpdateUserId = UserSeeder.ServiceUser.Id,
                UpdatedAt = DateTime.UtcNow,
            } };
            user3.Roles = new List<UserRole> { new UserRole() {
                User = user3,
                RoleId = RoleSeeder.Role5Deliverer.Id,
                UpdateUserId = UserSeeder.ServiceUser.Id,
                UpdatedAt = DateTime.UtcNow,
            } };

            return new List<User> { user1, user2, user3 };
        }

        public static List<Order> CreateOrdersForCompleteTest(TestsToolkit toolkit)
        {
            var builder = toolkit.Resolve<OrderNameBuilderService>();

            var order1 = new Order
            {
                AddressId = WarehouseSeeder.Warehouse1PL.Id,
                Name = builder.Build(1),
                Percentage = 0,
                UpdateUserId = UserSeeder.ServiceUser.Id,
                UpdatedAt = DateTime.UtcNow,
                OwnerUserId = UserSeeder.ServiceUser.Id,
                CreationDate = DateTime.UtcNow,
                IsCanceledByUser = true,
                OrderProducts = new List<OrderProduct>
                {
                    new OrderProduct
                    {
                        UpdateUserId = UserSeeder.ServiceUser.Id,
                        UpdatedAt = DateTime.UtcNow,
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
                UpdateUserId = UserSeeder.ServiceUser.Id,
                UpdatedAt = DateTime.UtcNow,
                OwnerUserId = UserSeeder.ServiceUser.Id,
                CreationDate = DateTime.UtcNow,
                IsCanceledByUser = false,
                OrderProducts = new List<OrderProduct>
                {
                    new OrderProduct
                    {
                        UpdateUserId = UserSeeder.ServiceUser.Id,
                        UpdatedAt = DateTime.UtcNow,
                        ProductId = ProductSeeder.Product9Milk.Id,
                        Quantity = 30
                    },
                    new OrderProduct
                    {
                        UpdateUserId = UserSeeder.ServiceUser.Id,
                        UpdatedAt = DateTime.UtcNow,
                        ProductId = ProductSeeder.Product1Rice.Id,
                        Quantity = 200
                    },
                    new OrderProduct
                    {
                        UpdateUserId = UserSeeder.ServiceUser.Id,
                        UpdatedAt = DateTime.UtcNow,
                        ProductId = ProductSeeder.Product2Pasta.Id,
                        Quantity = 20
                    },
                    new OrderProduct
                    {
                        UpdateUserId = UserSeeder.ServiceUser.Id,
                        UpdatedAt = DateTime.UtcNow,
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
                UpdateUserId = UserSeeder.ServiceUser.Id,
                UpdatedAt = DateTime.UtcNow,
                WarehouseId = WarehouseSeeder.Warehouse1PL.Id,
                ProductId = ProductSeeder.Product9Milk.Id,
                Quantity = 100,
            };

            var w1Rice = new Stock
            {
                UpdateUserId = UserSeeder.ServiceUser.Id,
                UpdatedAt = DateTime.UtcNow,
                WarehouseId = WarehouseSeeder.Warehouse1PL.Id,
                ProductId = ProductSeeder.Product1Rice.Id,
                Quantity = 100,
            };

            var w2Milk = new Stock
            {
                UpdateUserId = UserSeeder.ServiceUser.Id,
                UpdatedAt = DateTime.UtcNow,
                WarehouseId = WarehouseSeeder.Warehouse2PL.Id,
                ProductId = ProductSeeder.Product9Milk.Id,
                Quantity = 50,
            };

            var w2Pasta = new Stock
            {
                UpdateUserId = UserSeeder.ServiceUser.Id,
                UpdatedAt = DateTime.UtcNow,
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
                UpdateUserId = UserSeeder.ServiceUser.Id,
                UpdatedAt = DateTime.UtcNow,
                WarehouseId = WarehouseSeeder.Warehouse2PL.Id,
                ProductId = ProductSeeder.Product1Rice.Id,
                Quantity = 300,
            };

            var w3Cereals = new Stock
            {
                UpdateUserId = UserSeeder.ServiceUser.Id,
                UpdatedAt = DateTime.UtcNow,
                WarehouseId = WarehouseSeeder.Warehouse3PL.Id,
                ProductId = ProductSeeder.Product3Cereals.Id,
                Quantity = 50,
            };

            return new List<Stock> { w2Rice, w3Cereals };
        }
    }
}