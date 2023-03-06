using Autofac;
using Core;
using Core.Actions.MyNotifications;
using Core.Database;
using Core.Setup.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestsBase;

namespace CoreTests.Actions.MyNotificationsGetListLast30DaysTest
{
    [TestClass()]
    public class MyNotificationsGetListLast30DaysActionTest
    {
        private readonly List<Module> _usedModules = new()
        {
            new CoreModule(WebApiUserProviderType.None)
        };

        [TestMethod()]
        public async Task MyNotificationsGetListLast30DaysActionTest_Return2NotificationsInLast30Days()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<MyNotificationsGetListLast30DaysAction>();

            // Arrange
            var user1 = MyNotificationsGetListLast30DaysFixture.CreateUser("Normal", "User");
            var auth0User1 = MyNotificationsGetListLast30DaysFixture.CreateAuth0User(user1, 1);

            var notification35daysAgo = MyNotificationsGetListLast30DaysFixture.CreateNotifaction(user1, 35, "I won't work");
            var notification5daysAgo = MyNotificationsGetListLast30DaysFixture.CreateNotifaction(user1, 5, "I will work");
            var notification29daysAgo = MyNotificationsGetListLast30DaysFixture.CreateNotifaction(user1, 29, "I will work too");
            var notification50daysAgo = MyNotificationsGetListLast30DaysFixture.CreateNotifaction(user1, 50, "I won't work too");

            context.Add(user1);
            context.Add(notification35daysAgo);
            context.Add(notification5daysAgo);
            context.Add(notification29daysAgo);
            context.Add(notification50daysAgo);

            await context.SaveChangesAsync();
            // Act
            var executed = await action.Execute();
            var result = executed.Value as List<MyNotificationsGetListLast30DaysResponseItem>;

            // Assert
            //Assert.AreEqual(2, result.);
            Assert.AreEqual(2, context.Notifications.Count(c => c.CreationDate > DateTime.UtcNow.AddDays(-30)));
            Assert.AreEqual(4, context.Notifications.Count());
        }
    }
}
