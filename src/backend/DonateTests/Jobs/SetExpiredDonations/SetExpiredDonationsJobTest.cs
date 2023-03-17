using Autofac;
using Core;
using Core.Database;
using Core.Database.Enums;
using Core.Setup.Enums;
using Donate;
using Donate.Jobs.SetExpiredDonations;
using Donate.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestsBase;

namespace DonateTests.Jobs.SetExpiredDonations
{
    [TestClass()]
    public class SetExpiredDonationsJobTests
    {
        private readonly List<Module> _usedModules = new()
        {
            new CoreModule(WebApiUserProviderType.None),
            new DonateModule(),
        };

        [TestMethod()]
        public async Task SetExpiredDonationsJobTest()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var job = toolkit.Resolve<SetExpiredDonationsJob>();

            // Arrange
            var donation31DaysDelivered = SetExpiredDonationsJobFixture.CreateDonation("DNT000001");
            var donation5DaysDelivered = SetExpiredDonationsJobFixture.CreateDonation("DNT000002");
            var donation31DaysNotDelivered = SetExpiredDonationsJobFixture.CreateDonation("DNT000003");
            var donation5DaysNotDelivered = SetExpiredDonationsJobFixture.CreateDonation("DNT000004");
            var donation31DaysExpired = SetExpiredDonationsJobFixture.CreateDonation("DNT000005");

            var offset = ExpireDateService.ExpireDays + 1;

            donation31DaysDelivered.IsDelivered = true;
            donation5DaysDelivered.IsDelivered = true;
            donation31DaysDelivered.CreationDate = DateTime.UtcNow.AddDays(-offset);
            donation31DaysDelivered.ExpirationDate = DateTime.UtcNow.AddDays(-1);
            donation31DaysNotDelivered.CreationDate = DateTime.UtcNow.AddDays(-offset);
            donation31DaysNotDelivered.ExpirationDate = DateTime.UtcNow.AddDays(-1);
            donation31DaysExpired.CreationDate = DateTime.UtcNow.AddDays(-offset);
            donation31DaysExpired.ExpirationDate = DateTime.UtcNow.AddDays(-1);
            donation31DaysExpired.IsExpired = true;
            donation31DaysExpired.Status = DbEntityStatus.Inactive;

            Assert.IsTrue(ExpireDateService.GetExpiredDate4Donation(donation31DaysExpired) > donation31DaysExpired.CreationDate);
            Assert.IsTrue(ExpireDateService.GetExpiredDate4Donation(donation31DaysExpired.CreationDate) > donation31DaysExpired.CreationDate);

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
