using Autofac;
using Core;
using Core.Actions.DonateForm.GetProducts;
using Core.Database;
using Core.Setup.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestsBase;

namespace DonateTests.Services.GetProductsActionTest
{
    [TestClass()]
    public class GetProductsActionTest
    {
        private readonly List<Module> _usedModules = new()
        {
            new CoreModule(WebApiUserProviderType.None),
        };

        [TestMethod()]
        public async Task GetProductsActionTest_GeProducts_ReturnsProducts()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<GetProductsAction>();

            // Act
            var executed = await action.Execute();
            var result = executed.Value as List<GetProductsResponse>;

            // Assert
            Assert.IsTrue(result!.Any());
        }
    }
}
