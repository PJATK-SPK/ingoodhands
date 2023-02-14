using Core.Database.Enums;
using Core.Database.Models.Auth;
using Core.Database;
using Core.Setup.Auth0;
using Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestsBase;
using Autofac;
using Donate;
using Core.Setup.Enums;
using Donate.Jobs.SetExpiredDonations;

namespace DonateTests.Jobs.SetExpiredDonations
{
    [TestClass()]
    public class SetExpiredDonationsJobTests
    {
        private readonly SetExpiredDonationsJobFixture _fixture;

        public SetExpiredDonationsJobTests()
        {
            _fixture = new SetExpiredDonationsJobFixture();
        }

        private readonly List<Module> _usedModules = new()
        {
            new CoreModule(WebApiUserProviderType.None),
            new DonateModule(),
        };

        [TestMethod()]
        public async Task PostLoginActionTest_UserAndAuth0UserPresent()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var job = toolkit.Resolve<SetExpiredDonationsJob>();

            // Arrange
            var donation31DaysDelivered = _fixture.CreateDonation("DNT000001");
            var donation5DaysDelivered = _fixture.CreateDonation("DNT000002");
            var donation31DaysNotDelivered = _fixture.CreateDonation("DNT000003");
            var donation5DaysNotDelivered = _fixture.CreateDonation("DNT000004");
            var donation31DaysExpired = _fixture.CreateDonation("DNT000003");

            donation31DaysDelivered.IsDelivered = true;
            donation5DaysDelivered.IsDelivered = true;
            donation31DaysDelivered.CreationDate = DateTime.UtcNow.AddDays(-31);
            donation31DaysNotDelivered.CreationDate = DateTime.UtcNow.AddDays(-31);
            donation31DaysExpired.CreationDate = DateTime.UtcNow.AddDays(-31);
            donation31DaysExpired.IsExpired = true;
            donation31DaysExpired.Status = DbEntityStatus.Inactive;

            context.Add(donation31DaysDelivered);
            context.Add(donation5DaysDelivered);
            context.Add(donation31DaysNotDelivered);
            context.Add(donation5DaysNotDelivered);
            context.Add(donation31DaysExpired);

            await context.SaveChangesAsync();

            // Act
            var result = await job.Execute();

            // Assert
            Assert.IsFalse(donation31DaysDelivered.IsExpired);
            Assert.IsFalse(donation5DaysDelivered.IsExpired);
            Assert.IsTrue(donation31DaysNotDelivered.IsExpired);
            Assert.IsFalse(donation5DaysNotDelivered.IsExpired);
            Assert.IsTrue(donation31DaysExpired.IsExpired);

            Assert.AreEqual(DbEntityStatus.Active, donation31DaysDelivered.Status);
            Assert.AreEqual(DbEntityStatus.Active, donation5DaysDelivered.Status);
            Assert.AreEqual(DbEntityStatus.Inactive, donation31DaysNotDelivered.Status);
            Assert.AreEqual(DbEntityStatus.Active, donation5DaysNotDelivered.Status);
            Assert.AreEqual(DbEntityStatus.Inactive, donation31DaysExpired.Status);
        }
    }
}
