using Core.Setup.Enums;
using Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Database.Enums;
using Core.Database.Models.Auth;
using Core.Database;
using Core.Services;
using Core.Setup.Auth0;
using TestsBase;
using Autofac;

namespace CoreTests.Services
{
    [TestClass()]
    public class CounterServiceTests
    {
        private readonly List<Module> _usedModules = new()
        {
            new CoreModule(WebApiUserProviderType.None)
        };

        [TestMethod()]
        public async Task CounterServiceTests_DonationTest_ReturnCounterOfDonationTable()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<CounterService>();

            // Arrange
            var donationTableName = "Donations";

            // Act
            var result = await action.GetCounter(donationTableName);

            // Assert
            Assert.AreEqual(0, result.Value);
        }

        [TestMethod()]
        public async Task CounterServiceTests_UpdateCounter_ReturnUpdatedCounterOfDonationTable()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var action = toolkit.Resolve<CounterService>();

            // Arrange
            var donationTableName = "Donations";

            // Act
            var result = action.GetAndUpdateNextCounter(donationTableName);

            // Assert
            Assert.AreEqual(1, result);
        }
    }
}
