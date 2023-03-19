using Autofac;
using Core;
using Core.Database;
using Core.Database.Seeders;
using Core.Setup.Auth0;
using Core.Setup.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orders;
using Orders.Actions.StocksActions.StocksGetList;
using OrdersTests.Actions.Stock;
using System.Linq.Dynamic.Core;
using TestsBase;

namespace OrdersTests.Actions.Stock
{
    [TestClass()]
    public class StocksGetListTest
    {
        private readonly List<Module> _usedModules = new()
        {
            new CoreModule(WebApiUserProviderType.None),
            new OrdersModule(),
        };

        [TestMethod()]
        public async Task StocksGetListTest_ByUserWarehouseId_ReturnsResponse()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<StocksGetListAction>();

            //Arrange
            var page = 1;
            var pageSize = 10;

            var testingUser1 = StocksGetListFixture.CreateUser("Normal", "User", WarehouseSeeder.Warehouse1PL.Id);
            var testingAuth0User1 = StocksGetListFixture.CreateAuth0User(testingUser1, 1);
            var testUser1Role1 = StocksGetListFixture.CreateUserRole(testingUser1, RoleSeeder.Role4WarehouseKeeper.Id);
            var stock1 = StocksGetListFixture.CreateStock(ProductSeeder.Product4Groats, 50, (long)testingUser1.WarehouseId!);
            var stock2 = StocksGetListFixture.CreateStock(ProductSeeder.Product2Pasta, 10, (long)testingUser1.WarehouseId!);
            var stock3 = StocksGetListFixture.CreateStock(ProductSeeder.Product11Juice, 100, WarehouseSeeder.Warehouse5DE.Id);

            context.Add(testingUser1);
            context.Add(testingAuth0User1);
            context.Add(testUser1Role1);
            context.Add(stock1);
            context.Add(stock2);
            context.Add(stock3);

            await context.SaveChangesAsync();

            toolkit.UpdateUserInfo(StocksGetListFixture.GetCurrentUserInfo(testingAuth0User1));

            // Act
            var executed = await action.Execute(page, pageSize);
            var result = executed.Value as PagedResult<StocksGetListItemResponse>;

            // Assert            
            Assert.IsNotNull(result);
            Assert.AreEqual(page, result.CurrentPage);
            Assert.AreEqual(pageSize, result.PageSize);
            Assert.IsTrue(result!.Queryable.Any());
            Assert.AreEqual(2, result!.Queryable.Count());
            Assert.AreEqual(60, result!.Queryable.Where(c => toolkit.Hashids.DecodeSingleLong(c.WarehouseId) == testingUser1.WarehouseId).Sum(c => c.Quantity));
        }
    }
}
