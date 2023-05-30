using Autofac;
using Core;
using Core.Actions.MyNotifications.TestWebPush;
using Core.Actions.MyNotifications.UpdateWebPush;
using Core.Database;
using Core.Setup.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestsBase;

namespace CoreTests.Actions.MyNotificationsTestWebPush
{
    [TestClass()]
    public class MyNotificationsTestWebPushTest
    {
        private readonly List<Module> _usedModules = new()
        {
            new CoreModule(WebApiUserProviderType.None)
        };

        [TestMethod()]
        public async Task Test_with_WebPush()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var updateAction = toolkit.Resolve<MyNotificationsUpdateWebPushAction>();
            var testAction = toolkit.Resolve<MyNotificationsTestWebPushAction>();

            // Arrange
            var user = MyNotificationsTestWebPushFixture.CreateUser("Normal", "User");
            var auth0User = MyNotificationsTestWebPushFixture.CreateAuth0User(user, 1);
            context.Add(user);
            context.Add(auth0User);

            await context.SaveChangesAsync();
            toolkit.UpdateUserInfo(MyNotificationsTestWebPushFixture.GetCurrentUserInfo(auth0User));

            var endpointName = "endpoint1";
            var authName = "auth";
            var p256dhName = "p256";

            // Act
            await updateAction.Execute(new MyNotificationsUpdateWebPushPayload { Endpoint = endpointName, Auth = authName, P256dh = p256dhName });
            await testAction.Execute();

            var entry = context.UsersWebPush.Single();

            // Assert
            Assert.AreEqual(endpointName, entry.Endpoint);
            Assert.AreEqual(user.Id, entry.UserId);
        }

        [TestMethod()]
        public async Task Test_without_WebPush()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var testAction = toolkit.Resolve<MyNotificationsTestWebPushAction>();

            // Arrange
            var user = MyNotificationsTestWebPushFixture.CreateUser("Normal", "User");
            var auth0User = MyNotificationsTestWebPushFixture.CreateAuth0User(user, 1);
            context.Add(user);
            context.Add(auth0User);

            await context.SaveChangesAsync();
            toolkit.UpdateUserInfo(MyNotificationsTestWebPushFixture.GetCurrentUserInfo(auth0User));

            // Act
            await testAction.Execute();
        }
    }
}
