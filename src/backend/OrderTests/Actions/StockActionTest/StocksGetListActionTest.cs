using Autofac;
using Core;
using Core.Database;
using Core.Database.Seeders;
using Core.Exceptions;
using Core.Setup.Auth0;
using Core.Setup.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orders;
using Orders.Actions.StocksActions.StocksGetList;
using System.Linq.Dynamic.Core;
using TestsBase;

namespace OrdersTests.Actions.StockActionTest
{
    [TestClass()]
    public class StocksGetListActionTest
    {
        private readonly List<Module> _usedModules = new()
        {
            new CoreModule(WebApiUserProviderType.None),
            new OrdersModule(),
        };

        [TestMethod()]
        public async Task StocksGetListActionTest_StocksGetList_ReturnsResponse()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<StocksGetListAction>();

            //Arrange
            var page = 1;
            var pageSize = 10;

            var testingUser1 = StocksGetListActionFixture.CreateUser("Normal", "User");
            var testingAuth0User1 = StocksGetListActionFixture.CreateAuth0User(testingUser1, 1);
            var testUser1Role1 = StocksGetListActionFixture.CreateUserRole(testingUser1, RoleSeeder.Role4WarehouseKeeper.Id);
            var stock1 = StocksGetListActionFixture.CreateStock(ProductSeeder.Product4Groats, 50);
            var stock2 = StocksGetListActionFixture.CreateStock(ProductSeeder.Product2Pasta, 10);
            var stock3 = StocksGetListActionFixture.CreateStock(ProductSeeder.Product11Juice, 100);

            context.Add(testingUser1);
            context.Add(testingAuth0User1);
            context.Add(testUser1Role1);
            context.Add(stock1);
            context.Add(stock2);
            context.Add(stock3);

            await context.SaveChangesAsync();

            toolkit.UpdateUserInfo(new CurrentUserInfo
            {
                Email = testingAuth0User1.Email,
                EmailVerified = true,
                Identifier = testingAuth0User1.Identifier,
                GivenName = testingAuth0User1.FirstName,
                FamilyName = testingAuth0User1.LastName,
                Locale = "pl",
                Name = testingAuth0User1.FirstName + testingAuth0User1.LastName,
                Nickname = testingAuth0User1.Nickname,
                UpdatedAt = DateTime.UtcNow,
            });

            // Act
            var executed = await action.Execute(page, pageSize);
            var result = executed.Value as PagedResult<StocksGetListItemResponse>;

            // Assert            
            Assert.IsNotNull(result);
            Assert.AreEqual(page, result.CurrentPage);
            Assert.AreEqual(pageSize, result.PageSize);
            Assert.IsTrue(result!.Queryable.Any());
            Assert.AreEqual(3, result!.Queryable.Count());
        }

        [TestMethod()]
        public async Task StocksGetListActionTest_NoStocks_ThrowException()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<StocksGetListAction>();

            // Arrange
            var page = 1;
            var pageSize = 10;

            var testingUser1 = StocksGetListActionFixture.CreateUser("Normal", "User");
            var testingAuth0User1 = StocksGetListActionFixture.CreateAuth0User(testingUser1, 1);
            var testUser1Role1 = StocksGetListActionFixture.CreateUserRole(testingUser1, RoleSeeder.Role4WarehouseKeeper.Id);

            context.Add(testingUser1);
            context.Add(testingAuth0User1);
            context.Add(testUser1Role1);

            await context.SaveChangesAsync();

            toolkit.UpdateUserInfo(new CurrentUserInfo
            {
                Email = testingAuth0User1.Email,
                EmailVerified = true,
                Identifier = testingAuth0User1.Identifier,
                GivenName = testingAuth0User1.FirstName,
                FamilyName = testingAuth0User1.LastName,
                Locale = "pl",
                Name = testingAuth0User1.FirstName + testingAuth0User1.LastName,
                Nickname = testingAuth0User1.Nickname,
                UpdatedAt = DateTime.UtcNow,
            });

            // Act
            var exception = await Assert.ThrowsExceptionAsync<ItemNotFoundException>(() => action.Execute(page, pageSize));

            // Assert
            Assert.IsInstanceOfType(exception, typeof(ItemNotFoundException));
            Assert.IsNotNull(exception.Message);
        }
    }
}
