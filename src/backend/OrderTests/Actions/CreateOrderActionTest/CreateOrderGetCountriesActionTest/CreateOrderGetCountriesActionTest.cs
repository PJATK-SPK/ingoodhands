using Autofac;
using Core;
using Core.Database;
using Core.Database.Models.Core;
using Core.Setup.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orders;
using Orders.Actions.CreateOrderActions.CreateOrderGetCountries;
using TestsBase;

namespace OrderTests.Actions.CreateOrderActionTest.CreateOrderGetCountriesActionTest
{
    [TestClass()]
    public class CreateOrderGetCountriesActionTest
    {
        private readonly List<Module> _usedModules = new()
        {
            new CoreModule(WebApiUserProviderType.None),
            new OrdersModule(),
        };

        [TestMethod()]
        public async Task CreateOrderGetCountriesActionTest_GetCountriesAction_ReturnsListOfCountries()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<CreateOrderGetCountriesAction>();

            // Arrange & Act
            var executed = await action.Execute();
            var result = executed.Value as List<string>;

            // Assert            
            Assert.IsNotNull(result);
            Assert.IsTrue(result!.Any());
            Assert.AreEqual(247, result.Count);
        }
    }
}
