using Core.Database.Enums;
using Core.Database.Models.Auth;
using Core.Database.Models.Core;
using Core.Database.Seeders;
using Orders.Services.DeliveryNameBuilder;
using Orders.Services.OrderNameBuilder;
using TestsBase;

namespace OrdersTests.Jobs.RecalcOrdersPercentage
{
    public class RecalcOrdersPercentageJobFixture
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
                WarehouseId = WarehouseSeeder.Warehouse4DE.Id
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
                },
                Deliveries = new List<Delivery>()
            };

            var order2 = new Order
            {
                AddressId = WarehouseSeeder.Warehouse4DE.Id,
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
                        Quantity = 200
                    },
                    new OrderProduct
                    {
                        UpdateUserId = UserSeeder.ServiceUser.Id,
                        UpdatedAt = DateTime.UtcNow,
                        ProductId = ProductSeeder.Product1Rice.Id,
                        Quantity = 200
                    }
                },
                Deliveries = new List<Delivery>()
            };

            var order3 = new Order
            {
                AddressId = WarehouseSeeder.Warehouse1PL.Id,
                Name = builder.Build(3),
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
                        ProductId = ProductSeeder.Product2Pasta.Id,
                        Quantity = 100
                    },
                },
                Deliveries = new List<Delivery>()
            };

            var order4 = new Order
            {
                AddressId = WarehouseSeeder.Warehouse1PL.Id,
                Name = builder.Build(4),
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
                        Quantity = 350
                    },
                    new OrderProduct
                    {
                        UpdateUserId = UserSeeder.ServiceUser.Id,
                        UpdatedAt = DateTime.UtcNow,
                        ProductId = ProductSeeder.Product1Rice.Id,
                        Quantity = 50
                    },
                    new OrderProduct
                    {
                        UpdateUserId = UserSeeder.ServiceUser.Id,
                        UpdatedAt = DateTime.UtcNow,
                        ProductId = ProductSeeder.Product12EnergyDrink.Id,
                        Quantity = 150
                    },
                    new OrderProduct
                    {
                        UpdateUserId = UserSeeder.ServiceUser.Id,
                        UpdatedAt = DateTime.UtcNow,
                        ProductId = ProductSeeder.Product5Walnuts.Id,
                        Quantity = 50
                    }
                },
                Deliveries = new List<Delivery>()
            };

            return new List<Order> { order1, order2, order3, order4 };
        }

        public static List<Delivery> CreateDeliveryForCompleteTestPart1(TestsToolkit toolkit, Order order2, Order order3, Order order4, User user1, User user2)
        {
            var builder = toolkit.Resolve<DeliveryNameBuilderService>();

            var delivery1 = new Delivery
            {
                Name = builder.Build(1),
                Order = order2,
                IsDelivered = true,
                DelivererUser = user1,
                CreationDate = DateTime.UtcNow,
                IsLost = false,
                TripStarted = true,
                WarehouseId = WarehouseSeeder.Warehouse4DE.Id,
                DeliveryProducts = new List<DeliveryProduct>
                {
                    new DeliveryProduct
                    {
                        UpdateUserId = UserSeeder.ServiceUser.Id,
                        UpdatedAt = DateTime.UtcNow,
                        ProductId = ProductSeeder.Product9Milk.Id,
                        Quantity = 100
                    },
                    new DeliveryProduct
                    {
                        UpdateUserId = UserSeeder.ServiceUser.Id,
                        UpdatedAt = DateTime.UtcNow,
                        ProductId = ProductSeeder.Product1Rice.Id,
                        Quantity = 100
                    }
                },
                UpdateUserId = UserSeeder.ServiceUser.Id,
                UpdatedAt = DateTime.UtcNow,
            };

            var delivery2 = new Delivery
            {
                Name = builder.Build(2),
                Order = order3,
                IsDelivered = true,
                DelivererUser = user2,
                CreationDate = DateTime.UtcNow,
                IsLost = false,
                TripStarted = true,
                WarehouseId = WarehouseSeeder.Warehouse2PL.Id,
                DeliveryProducts = new List<DeliveryProduct>
                {
                    new DeliveryProduct
                    {
                        UpdateUserId = UserSeeder.ServiceUser.Id,
                        UpdatedAt = DateTime.UtcNow,
                        ProductId = ProductSeeder.Product2Pasta.Id,
                        Quantity = 100
                    }
                },
                UpdateUserId = UserSeeder.ServiceUser.Id,
                UpdatedAt = DateTime.UtcNow,
            };

            var delivery3 = new Delivery
            {
                Name = builder.Build(3),
                Order = order4,
                IsDelivered = true,
                DelivererUser = user2,
                CreationDate = DateTime.UtcNow,
                IsLost = false,
                TripStarted = true,
                WarehouseId = WarehouseSeeder.Warehouse3PL.Id,
                DeliveryProducts = new List<DeliveryProduct>
                {
                    new DeliveryProduct
                    {
                        UpdateUserId = UserSeeder.ServiceUser.Id,
                        UpdatedAt = DateTime.UtcNow,
                        ProductId = ProductSeeder.Product9Milk.Id,
                        Quantity = 50
                    },
                    new DeliveryProduct
                    {
                        UpdateUserId = UserSeeder.ServiceUser.Id,
                        UpdatedAt = DateTime.UtcNow,
                        ProductId = ProductSeeder.Product1Rice.Id,
                        Quantity = 50
                    },
                    new DeliveryProduct
                    {
                        UpdateUserId = UserSeeder.ServiceUser.Id,
                        UpdatedAt = DateTime.UtcNow,
                        ProductId = ProductSeeder.Product12EnergyDrink.Id,
                        Quantity = 50
                    },
                    new DeliveryProduct
                    {
                        UpdateUserId = UserSeeder.ServiceUser.Id,
                        UpdatedAt = DateTime.UtcNow,
                        ProductId = ProductSeeder.Product5Walnuts.Id,
                        Quantity = 50
                    }
                },
                UpdateUserId = UserSeeder.ServiceUser.Id,
                UpdatedAt = DateTime.UtcNow,
            };

            return new List<Delivery> { delivery1, delivery2, delivery3 };
        }

        public static List<Delivery> CreateDeliveryForCompleteTestPart2(TestsToolkit toolkit, Order order2, Order order4, User user1, User user2)
        {
            var builder = toolkit.Resolve<DeliveryNameBuilderService>();

            var delivery1 = new Delivery
            {
                Name = builder.Build(4),
                Order = order2,
                IsDelivered = false,
                DelivererUser = user1,
                CreationDate = DateTime.UtcNow,
                IsLost = true,
                TripStarted = false,
                WarehouseId = WarehouseSeeder.Warehouse4DE.Id,
                DeliveryProducts = new List<DeliveryProduct>
                {
                    new DeliveryProduct
                    {
                        UpdateUserId = UserSeeder.ServiceUser.Id,
                        UpdatedAt = DateTime.UtcNow,
                        ProductId = ProductSeeder.Product1Rice.Id,
                        Quantity = 100
                    }
                },
                UpdateUserId = UserSeeder.ServiceUser.Id,
                UpdatedAt = DateTime.UtcNow,
            };

            var delivery2 = new Delivery
            {
                Name = builder.Build(5),
                Order = order4,
                IsDelivered = true,
                DelivererUser = user2,
                CreationDate = DateTime.UtcNow,
                IsLost = false,
                TripStarted = true,
                WarehouseId = WarehouseSeeder.Warehouse3PL.Id,
                DeliveryProducts = new List<DeliveryProduct>
                {
                    new DeliveryProduct
                    {
                        UpdateUserId = UserSeeder.ServiceUser.Id,
                        UpdatedAt = DateTime.UtcNow,
                        ProductId = ProductSeeder.Product9Milk.Id,
                        Quantity = 50
                    },
                    new DeliveryProduct
                    {
                        UpdateUserId = UserSeeder.ServiceUser.Id,
                        UpdatedAt = DateTime.UtcNow,
                        ProductId = ProductSeeder.Product12EnergyDrink.Id,
                        Quantity = 50
                    },
                },
                UpdateUserId = UserSeeder.ServiceUser.Id,
                UpdatedAt = DateTime.UtcNow,
            };

            return new List<Delivery> { delivery1, delivery2 };
        }

        public static List<Delivery> CreateDeliveryForCompleteTestPart3(TestsToolkit toolkit, Order order2, Order order4, User user1, User user2)
        {
            var builder = toolkit.Resolve<DeliveryNameBuilderService>();

            var delivery1 = new Delivery
            {
                Name = builder.Build(6),
                Order = order2,
                IsDelivered = true,
                DelivererUser = user1,
                CreationDate = DateTime.UtcNow,
                IsLost = false,
                TripStarted = true,
                WarehouseId = WarehouseSeeder.Warehouse4DE.Id,
                DeliveryProducts = new List<DeliveryProduct>
                {
                    new DeliveryProduct
                    {
                        UpdateUserId = UserSeeder.ServiceUser.Id,
                        UpdatedAt = DateTime.UtcNow,
                        ProductId = ProductSeeder.Product9Milk.Id,
                        Quantity = 100
                    },
                    new DeliveryProduct
                    {
                        UpdateUserId = UserSeeder.ServiceUser.Id,
                        UpdatedAt = DateTime.UtcNow,
                        ProductId = ProductSeeder.Product1Rice.Id,
                        Quantity = 100
                    }
                },
                UpdateUserId = UserSeeder.ServiceUser.Id,
                UpdatedAt = DateTime.UtcNow,
            };

            var delivery2 = new Delivery
            {
                Name = builder.Build(7),
                Order = order4,
                IsDelivered = true,
                DelivererUser = user2,
                CreationDate = DateTime.UtcNow,
                IsLost = false,
                TripStarted = true,
                WarehouseId = WarehouseSeeder.Warehouse3PL.Id,
                DeliveryProducts = new List<DeliveryProduct>
                {
                    new DeliveryProduct
                    {
                        UpdateUserId = UserSeeder.ServiceUser.Id,
                        UpdatedAt = DateTime.UtcNow,
                        ProductId = ProductSeeder.Product9Milk.Id,
                        Quantity = 250
                    },
                    new DeliveryProduct
                    {
                        UpdateUserId = UserSeeder.ServiceUser.Id,
                        UpdatedAt = DateTime.UtcNow,
                        ProductId = ProductSeeder.Product12EnergyDrink.Id,
                        Quantity = 50
                    },
                },
                UpdateUserId = UserSeeder.ServiceUser.Id,
                UpdatedAt = DateTime.UtcNow,
            };

            return new List<Delivery> { delivery1, delivery2 };
        }
    }
}