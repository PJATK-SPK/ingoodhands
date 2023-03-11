using Autofac;
using Core;
using Core.Actions.MyNotifications.GetList;
using Core.Actions.MyNotifications.UpdateWebPush;
using Core.Database;
using Core.Setup.Enums;
using CoreTests.Actions.MyNotificationsGetListLast30DaysTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestsBase;

namespace CoreTests.Actions.MyNotificationsUpdateWebPushTest
{
    [TestClass()]
    public class MyNotificationsUpdateWebPushActionTest
    {
        private readonly List<Module> _usedModules = new()
        {
            new CoreModule(WebApiUserProviderType.None)
        };

        [TestMethod()]
        public async Task Create_new_web_push()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<MyNotificationsUpdateWebPushAction>();

            // Arrange
            var user = MyNotificationsUpdateWebPushFixture.CreateUser("Normal", "User");
            var auth0User = MyNotificationsUpdateWebPushFixture.CreateAuth0User(user, 1);
            context.Add(user);
            context.Add(auth0User);

            await context.SaveChangesAsync();
            toolkit.UpdateUserInfo(MyNotificationsUpdateWebPushFixture.GetCurrentUserInfo(auth0User));

            var endpointName = "endpoint1";

            // Act
            await action.Execute(new MyNotificationsUpdateWebPushPayload { Endpoint = endpointName });

            var entry = context.UsersWebPush.Single();

            // Assert
            Assert.AreEqual(endpointName, entry.Endpoint);
            Assert.AreEqual(user.Id, entry.UserId);
        }

        [TestMethod()]
        public async Task Update_existing_new_web_push()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<MyNotificationsUpdateWebPushAction>();

            // Arrange
            var user = MyNotificationsUpdateWebPushFixture.CreateUser("Normal", "User");
            var auth0User = MyNotificationsUpdateWebPushFixture.CreateAuth0User(user, 1);
            context.Add(user);
            context.Add(auth0User);

            await context.SaveChangesAsync();
            toolkit.UpdateUserInfo(MyNotificationsUpdateWebPushFixture.GetCurrentUserInfo(auth0User));

            await context.SaveChangesAsync();

            var endpointName = "endpoint1";

            // Act
            await action.Execute(new MyNotificationsUpdateWebPushPayload { Endpoint = endpointName });

            endpointName = "endpoint2";

            await action.Execute(new MyNotificationsUpdateWebPushPayload { Endpoint = endpointName });

            var entry = context.UsersWebPush.Single();

            // Assert
            Assert.AreEqual(endpointName, entry.Endpoint);
            Assert.AreEqual(user.Id, entry.UserId);
        }
    }
}
