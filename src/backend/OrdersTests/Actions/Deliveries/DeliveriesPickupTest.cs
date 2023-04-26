using Core.Database;
using Core.Setup.Enums;
using Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orders.Actions.DeliveriesActions.DeliveriesGetList;
using Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsBase;
using Autofac;
using Orders.Actions.DeliveriesActions.DeliveriesPickup;
using Core.Database.Seeders;
using Core.Database.Models.Core;
using Orders.Actions.DeliveriesActions.DeliveriesGetSingle;
using Core.Database.Enums;
using Core.Exceptions;

namespace OrdersTests.Actions.Deliveries
{

    [TestClass()]
    public class DeliveriesPickupTest
    {
        private readonly List<Module> _usedModules = new()
        {
            new CoreModule(WebApiUserProviderType.None),
            new OrdersModule(),
        };

        [TestMethod()]
        public async Task Pickup_ChangeTripStarted()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<DeliveriesPickupAction>();

            // Arrange
            var testingUser1 = DeliveriesPickupFixture.CreateUser("Normal", "User");
            var testingAuth0User1 = DeliveriesPickupFixture.CreateAuth0User(testingUser1, 1);
            var testUser1Role1 = DeliveriesPickupFixture.CreateUserRole(testingUser1, RoleSeeder.Role4WarehouseKeeper.Id);
            var order = DeliveriesPickupFixture.CreateOrder(toolkit);
            var delivery1 = DeliveriesPickupFixture.CreateDelivery(testingUser1, false, WarehouseSeeder.Warehouse2PL, 1, order, toolkit); //active, tripStarted = false

            context.Add(testingUser1);
            context.Add(testingAuth0User1);
            context.Add(testUser1Role1);
            context.Add(order);
            context.Add(delivery1);
            await context.SaveChangesAsync();

            toolkit.UpdateUserInfo(DeliveriesPickupFixture.GetCurrentUserInfo(testingAuth0User1));
            var encodedDeliveryId = toolkit.Hashids.EncodeLong(delivery1.Id);

            // Act
            var result = await action.Execute(encodedDeliveryId);

            var delivery = context.Deliveries.Single(c => c.Id == delivery1.Id);

            // Assert            
            Assert.IsNotNull(result);
            Assert.IsTrue(delivery.TripStarted);
            Assert.AreEqual(DbEntityStatus.Active, delivery.Status);
        }

        [TestMethod()]
        public async Task PickupNotFoundDelivery_ThrowsException()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<DeliveriesPickupAction>();

            //Arrange
            var testingUser1 = DeliveriesPickupFixture.CreateUser("Normal", "User");
            var testingAuth0User1 = DeliveriesPickupFixture.CreateAuth0User(testingUser1, 1);
            var testUser1Role1 = DeliveriesPickupFixture.CreateUserRole(testingUser1, RoleSeeder.Role4WarehouseKeeper.Id);

            context.Add(testingUser1);
            context.Add(testingAuth0User1);
            context.Add(testUser1Role1);

            await context.SaveChangesAsync();

            toolkit.UpdateUserInfo(DeliveriesPickupFixture.GetCurrentUserInfo(testingAuth0User1));

            // Act
            var exception = await Assert.ThrowsExceptionAsync<ItemNotFoundException>(() => action.Execute(toolkit.Hashids.EncodeLong(1500)));

            // Assert
            Assert.IsInstanceOfType(exception, typeof(ItemNotFoundException));
            Assert.IsNotNull(exception.Message);
        }
    }
}