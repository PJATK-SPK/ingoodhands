using Core.Exceptions;
using Autofac;
using Core.Database;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestsBase;
using Donate.Actions.DonateForm.GetProducts;
using Core.Database.Enums;
using Core.Database.Models.Auth;
using Core.Setup.Auth0;
using Microsoft.AspNetCore.Mvc;
using Core.Database.Models.Core;
using System.Diagnostics.Metrics;
using System.Net;
using Donate;
using Core;
using Core.Setup.Enums;

namespace DonateTests.Services.GetProductsActionTest
{
    [TestClass()]
    public class GetProductsActionTest
    {
        private readonly List<Module> _usedModules = new()
        {
            new CoreModule(WebApiUserProviderType.None),
            new DonateModule(),
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
