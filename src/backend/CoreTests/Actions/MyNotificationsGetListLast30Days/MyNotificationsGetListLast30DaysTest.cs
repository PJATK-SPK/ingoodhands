using Autofac;
using Core;
using Core.Actions.MyNotifications.GetList;
using Core.Database;
using Core.Setup.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestsBase;

namespace CoreTests.Actions.MyNotificationsGetListLast30Days
{
    [TestClass()]
    public class MyNotificationsGetListLast30DaysTest
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
            var notification2daysAgo = MyNotificationsGetListLast30DaysFixture.CreateNotifaction(user1, 2, "I will work three");
            var notification1dayAgo = MyNotificationsGetListLast30DaysFixture.CreateNotifaction(user1, 1, "I will work four");

            context.Add(user1);
            context.Add(notification35daysAgo);
            context.Add(notification5daysAgo);
            context.Add(notification29daysAgo);
            context.Add(notification50daysAgo);
            context.Add(notification2daysAgo);
            context.Add(notification1dayAgo);

            await context.SaveChangesAsync();
            // Act
            var executed = await action.Execute();
            var result = executed.Value as List<MyNotificationsGetListLast30DaysResponseItem>;

            // Assert
            Assert.AreEqual(4, result!.Count);
            Assert.AreEqual(4, context.Notifications.Count(c => c.CreationDate > DateTime.UtcNow.AddDays(-30)));
            Assert.AreEqual(6, context.Notifications.Count());
        }
    }
}
