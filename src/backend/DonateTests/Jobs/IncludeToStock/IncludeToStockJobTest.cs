using Autofac;
using Core;
using Core.Database;
using Core.Database.Models.Core;
using Core.Database.Seeders;
using Core.Setup.Enums;
using Donate;
using Donate.Jobs.IncludeToStock;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestsBase;

namespace DonateTests.Jobs.IncludeToStock
{
    [TestClass()]
    public class IncludeToStockJobTest
    {
        private readonly List<Module> _usedModules = new()
        {
            new CoreModule(WebApiUserProviderType.None),
            new DonateModule(),
        };

        [TestMethod()]
        public async Task DonationStockIntegrationTest_AddNewProductToStock()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var job = toolkit.Resolve<IncludeToStockJob>();

            // Arrange
            var donation1 = IncludeToStockJobFixture.CreateDonation(true, false, "DNT000001");
            var donationProduct1 = IncludeToStockJobFixture.CreateDonationProduct(donation1.Id, ProductSeeder.Product5Walnuts.Id, 10);

            var donation2 = IncludeToStockJobFixture.CreateDonation(false, false, "DNT000002");
            var donationProduct2 = IncludeToStockJobFixture.CreateDonationProduct(donation2.Id, ProductSeeder.Product5Walnuts.Id, 90);

            var donation3 = IncludeToStockJobFixture.CreateDonation(false, true, "DNT000003");
            var donationProduct3 = IncludeToStockJobFixture.CreateDonationProduct(donation3.Id, ProductSeeder.Product13Soup.Id, 200);

            donation1.Products = new List<DonationProduct>
            {
                donationProduct1
            };

            donation2.Products = new List<DonationProduct>
            {
                donationProduct2
            };

            donation3.Products = new List<DonationProduct>
            {
                donationProduct3
            };

            context.Add(donation1);
            context.Add(donationProduct1);

            context.Add(donation2);
            context.Add(donationProduct2);

            context.Add(donation3);
            context.Add(donationProduct3);

            await context.SaveChangesAsync();

            // Act
            var result = await job.Execute();
            var allStock = await context.Stocks.ToListAsync();
            var allDonations = await context.Donations.ToListAsync();

            // Assert
            Assert.IsNotNull(allStock);
            Assert.AreEqual(10, allStock.SingleOrDefault(c => c.ProductId == ProductSeeder.Product5Walnuts.Id)!.Quantity);
            Assert.IsNull(allStock.SingleOrDefault(c => c.ProductId == ProductSeeder.Product13Soup.Id));
            Assert.AreEqual(1, allDonations.Where(c => c.IsDelivered && c.IsIncludedInStock).Count());
        }

        [TestMethod()]
        public async Task DonationStockIntegrationTest_UpdateQuantityOfExistingProduct()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var job = toolkit.Resolve<IncludeToStockJob>();

            // Arrange
            var donation1 = IncludeToStockJobFixture.CreateDonation(true, false, "DNT000001");
            var donationProduct1 = IncludeToStockJobFixture.CreateDonationProduct(donation1.Id, ProductSeeder.Product5Walnuts.Id, 10);

            var donation2 = IncludeToStockJobFixture.CreateDonation(true, false, "DNT000002");
            var donationProduct3 = IncludeToStockJobFixture.CreateDonationProduct(donation2.Id, ProductSeeder.Product5Walnuts.Id, 90);

            donation1.Products = new List<DonationProduct>
            {
                donationProduct1
            };

            donation2.Products = new List<DonationProduct>
            {
                donationProduct3
            };

            context.Add(donation1);
            context.Add(donationProduct1);

            context.Add(donation2);
            context.Add(donationProduct3);

            await context.SaveChangesAsync();

            // Act
            var result = await job.Execute();
            var allStock = await context.Stocks.ToListAsync();
            var allDonations = await context.Donations.ToListAsync();

            // Assert
            Assert.IsNotNull(allStock);
            Assert.AreEqual(100, allStock.SingleOrDefault(c => c.ProductId == ProductSeeder.Product5Walnuts.Id)!.Quantity);
            Assert.AreEqual(2, allDonations.Where(c => c.IsDelivered && c.IsIncludedInStock).Count());
        }

        [TestMethod()]
        public async Task DonationStockIntegrationTest_AddNewProductToStockAndUpdateAnotherProduct()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var job = toolkit.Resolve<IncludeToStockJob>();

            // Arrange
            var donation1 = IncludeToStockJobFixture.CreateDonation(true, false, "DNT000001");
            var donationProduct1 = IncludeToStockJobFixture.CreateDonationProduct(donation1.Id, ProductSeeder.Product2Pasta.Id, 20);
            var donationProduct2 = IncludeToStockJobFixture.CreateDonationProduct(donation1.Id, ProductSeeder.Product12EnergyDrink.Id, 35);

            var donation2 = IncludeToStockJobFixture.CreateDonation(true, false, "DNT000002");
            var donationProduct3 = IncludeToStockJobFixture.CreateDonationProduct(donation2.Id, ProductSeeder.Product2Pasta.Id, 50);

            donation1.Products = new List<DonationProduct>
            {
                donationProduct1,
                donationProduct2
            };

            donation2.Products = new List<DonationProduct>
            {
                donationProduct3
            };

            context.Add(donation1);
            context.Add(donationProduct1);
            context.Add(donationProduct2);

            context.Add(donation2);
            context.Add(donationProduct3);

            await context.SaveChangesAsync();

            // Act
            var result = await job.Execute();
            var stock = await context.Stocks.FirstOrDefaultAsync(s => s.ProductId == ProductSeeder.Product2Pasta.Id);
            var stock2 = await context.Stocks.FirstOrDefaultAsync(s => s.ProductId == ProductSeeder.Product12EnergyDrink.Id);
            var allDonations = await context.Donations.ToListAsync();

            // Assert
            Assert.IsNotNull(stock);
            Assert.IsNotNull(stock2);
            Assert.AreEqual(70, stock.Quantity);
            Assert.AreEqual(35, stock2.Quantity);
            Assert.AreEqual(2, allDonations.Where(c => c.IsDelivered && c.IsIncludedInStock).Count());
        }
    }
}
