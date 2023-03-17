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
        public async Task AddNewProductToWarehouseStockAndUpdateExisting_ExcludeUndelivered()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var job = toolkit.Resolve<IncludeToStockJob>();

            // Arrange
            var warehouseId1 = WarehouseSeeder.Warehouse1PL.Id;
            var warehouseId2 = WarehouseSeeder.Warehouse5DE.Id;
            var donation1 = IncludeToStockJobFixture.CreateDonation(true, false, warehouseId1, "DNT000001");
            var donationProduct1 = IncludeToStockJobFixture.CreateDonationProduct(donation1.Id, ProductSeeder.Product5Walnuts.Id, 10);

            var donation2 = IncludeToStockJobFixture.CreateDonation(true, false, warehouseId1, "DNT000002");
            var donationProduct2 = IncludeToStockJobFixture.CreateDonationProduct(donation2.Id, ProductSeeder.Product5Walnuts.Id, 45);

            var donation3 = IncludeToStockJobFixture.CreateDonation(true, false, warehouseId1, "DNT000003");
            var donationProduct3 = IncludeToStockJobFixture.CreateDonationProduct(donation3.Id, ProductSeeder.Product13Soup.Id, 200);

            var donation4 = IncludeToStockJobFixture.CreateDonation(true, false, warehouseId2, "DNT000004");
            var donationProduct4 = IncludeToStockJobFixture.CreateDonationProduct(donation4.Id, ProductSeeder.Product12EnergyDrink.Id, 500);

            var donation5 = IncludeToStockJobFixture.CreateDonation(false, true, warehouseId1, "DNT000005");
            var donationProduct5 = IncludeToStockJobFixture.CreateDonationProduct(donation4.Id, ProductSeeder.Product12EnergyDrink.Id, 500);

            var donation6 = IncludeToStockJobFixture.CreateDonation(false, false, warehouseId2, "DNT000006");
            var donationProduct6 = IncludeToStockJobFixture.CreateDonationProduct(donation4.Id, ProductSeeder.Product12EnergyDrink.Id, 500);

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

            donation4.Products = new List<DonationProduct>
            {
                donationProduct4
            };

            context.Add(donation1);
            context.Add(donationProduct1);

            context.Add(donation2);
            context.Add(donationProduct2);

            context.Add(donation3);
            context.Add(donationProduct3);

            context.Add(donation4);
            context.Add(donationProduct4);

            await context.SaveChangesAsync();

            // Act
            var result = await job.Execute();
            var allStockWarehouse1 = await context.Stocks.Where(c => c.WarehouseId == warehouseId1).ToListAsync();
            var allStockWarehouse2 = await context.Stocks.Where(c => c.WarehouseId == warehouseId2).ToListAsync();
            var allDonations = await context.Donations.ToListAsync();

            // Assert
            Assert.IsNotNull(allStockWarehouse1);
            Assert.IsNotNull(allStockWarehouse2);
            Assert.AreEqual(55, allStockWarehouse1.SingleOrDefault(c => c.ProductId == ProductSeeder.Product5Walnuts.Id)!.Quantity);
            Assert.AreEqual(200, allStockWarehouse1.SingleOrDefault(c => c.ProductId == ProductSeeder.Product13Soup.Id)!.Quantity);
            Assert.AreEqual(2, allStockWarehouse1.Count);
            Assert.AreEqual(500, allStockWarehouse2.SingleOrDefault(c => c.ProductId == ProductSeeder.Product12EnergyDrink.Id)!.Quantity);
            Assert.AreEqual(1, allStockWarehouse2.Count);
            Assert.IsNull(allStockWarehouse1.SingleOrDefault(c => c.ProductId == ProductSeeder.Product12EnergyDrink.Id));
            Assert.AreEqual(4, allDonations.Where(c => c.IsDelivered && c.IsIncludedInStock).Count());
        }

        [TestMethod()]
        public async Task UpdateQuantityOfExistingProduct()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var job = toolkit.Resolve<IncludeToStockJob>();

            // Arrange
            var warehouseId1 = WarehouseSeeder.Warehouse1PL.Id;
            var donation1 = IncludeToStockJobFixture.CreateDonation(true, false, warehouseId1, "DNT000001");
            var donationProduct1 = IncludeToStockJobFixture.CreateDonationProduct(donation1.Id, ProductSeeder.Product5Walnuts.Id, 10);

            var donation2 = IncludeToStockJobFixture.CreateDonation(true, false, warehouseId1, "DNT000002");
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
            Assert.AreEqual(1, allStock.Count);
            Assert.AreEqual(2, allDonations.Where(c => c.IsDelivered && c.IsIncludedInStock).Count());
        }

        [TestMethod()]
        public async Task AddNewProductToStockAndUpdateAnotherProduct()
        {
            using var toolkit = new TestsToolkit(_usedModules);
            var context = toolkit.Resolve<AppDbContext>();
            var job = toolkit.Resolve<IncludeToStockJob>();

            // Arrange
            var warehouseId1 = WarehouseSeeder.Warehouse1PL.Id;
            var donation1 = IncludeToStockJobFixture.CreateDonation(true, false, warehouseId1, "DNT000001");
            var donationProduct1 = IncludeToStockJobFixture.CreateDonationProduct(donation1.Id, ProductSeeder.Product2Pasta.Id, 20);
            var donationProduct2 = IncludeToStockJobFixture.CreateDonationProduct(donation1.Id, ProductSeeder.Product12EnergyDrink.Id, 35);

            var donation2 = IncludeToStockJobFixture.CreateDonation(true, false, warehouseId1, "DNT000002");
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
