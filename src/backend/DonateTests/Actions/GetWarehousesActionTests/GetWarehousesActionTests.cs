using Core.Exceptions;
using Autofac;
using Core.Database;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestsBase;
using Donate.Actions.DonateForm.GetWarehouses;
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
using Donate.Actions.DonateForm.GetProducts;

namespace DonateTests.Services.GetWarehousesActionTests
{
    [TestClass()]
    public class GetWarehousesActionTests
    {
        private readonly List<Module> _usedModules = new()
        {
            new CoreModule(WebApiUserProviderType.None),
            new DonateModule(),
        };

        [TestMethod()]
        public async Task GetWarehousesActionTest_GetWarehouses_ReturnsWareHouses()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<GetWarehousesAction>();

            // Act
            var executed = await action.Execute();
            var result = executed.Value as List<GetWarehousesResponse>;

            // Assert
            Assert.IsTrue(result!.Any());
        }
    }
}