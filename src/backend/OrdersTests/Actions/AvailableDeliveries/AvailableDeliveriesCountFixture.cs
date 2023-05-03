using Core.Database.Enums;
using Core.Database.Models.Auth;
using Core.Database.Models.Core;
using Core.Database.Seeders;
using Core.Setup.Auth0;
using Orders.Services.DeliveryNameBuilder;
using Orders.Services.OrderNameBuilder;
using TestsBase;

namespace OrdersTests.Actions.AvailableDeliveries
{
    public static class AvailableDeliveriesCountFixture
    {
        public static User CreateUser(string firstName, string lastName) => new()
        {
            Status = DbEntityStatus.Active,
            FirstName = firstName,
            LastName = lastName,
            Email = firstName + "@" + lastName + ".com",
            WarehouseId = WarehouseSeeder.Warehouse1PL.Id,
        };

        public static User CreateUser2(string firstName, string lastName) => new()
        {
            Status = DbEntityStatus.Active,
            FirstName = firstName,
            LastName = lastName,
            Email = firstName + "@" + lastName + ".com",
        };

        public static Auth0User CreateAuth0User(User user, int id) => new()
        {
            FirstName = "Auth",
            LastName = "Auth0User" + id,
            Nickname = "Auth0",
            UpdateUserId = UserSeeder.ServiceUser.Id,
            UpdatedAt = DateTime.UtcNow,
            Email = user.Email,
            Identifier = user.FirstName + user.LastName + "GoogleId",
            User = user,
            UserId = user.Id
        };

        public static UserRole CreateUserRole(User user, long roleId) => new()
        {
            RoleId = roleId,
            UserId = user.Id,
            User = user,
            UpdateUserId = UserSeeder.ServiceUser.Id,
            UpdatedAt = DateTime.UtcNow,
            Status = DbEntityStatus.Active
        };

        public static CurrentUserInfo GetCurrentUserInfo(Auth0User auth0User) => new()
        {
            Email = auth0User.Email,
            EmailVerified = true,
            Identifier = auth0User.Identifier,
            GivenName = auth0User.FirstName,
            FamilyName = auth0User.LastName,
            Locale = "pl",
            Name = auth0User.FirstName + auth0User.LastName,
            Nickname = auth0User.Nickname,
            UpdatedAt = DateTime.UtcNow,
        };

        public static Order CreateOrder(TestsToolkit toolkit)
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
                IsCanceledByUser = false,
                IsFinished = false,
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
                }
            };
            return order1;
        }

        public static Delivery CreateDelivery(User user, bool isDelivered, Warehouse warehouse, long deliveryNameNumber, Order order, TestsToolkit toolkit)
        {
            var builder = toolkit.Resolve<DeliveryNameBuilderService>();

            var delivery = new Delivery
            {
                Name = builder.Build(deliveryNameNumber),
                Order = order,
                IsDelivered = isDelivered,
                DelivererUser = user,
                CreationDate = DateTime.UtcNow,
                IsLost = false,
                TripStarted = false,
                WarehouseId = warehouse.Id,
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
                Status = DbEntityStatus.Active
            };

            return delivery;
        }

        public static Delivery CreateDelivery2(User user, bool isDelivered, Warehouse warehouse, long deliveryNameNumber, Order order, TestsToolkit toolkit)
        {
            var builder = toolkit.Resolve<DeliveryNameBuilderService>();

            var delivery = new Delivery
            {
                Name = builder.Build(deliveryNameNumber),
                Order = order,
                IsDelivered = isDelivered,
                DelivererUser = user,
                CreationDate = DateTime.UtcNow,
                IsLost = true,
                TripStarted = true,
                WarehouseId = warehouse.Id,
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
                Status = DbEntityStatus.Inactive
            };

            return delivery;
        }
    }
}