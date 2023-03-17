using Core.Database;
using Core.Database.Enums;
using Core.Database.Models.Core;
using Core.Database.Seeders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Donate.Jobs.IncludeToStock
{
    public class IncludeToStockJob
    {
        private readonly AppDbContext _appDbContext;

        public IncludeToStockJob(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<ActionResult> Execute()
        {
            var deliveredNotIncludedInStockDonations = await _appDbContext.Donations
                .Include(c => c.Products)
                .Where(c => c.IsDelivered && !c.IsIncludedInStock)
                .ToListAsync();

            var allStock = await _appDbContext.Stocks
                .ToDictionaryAsync(c => (c.ProductId, c.WarehouseId), new StockKeyEqualityComparer());

            foreach (var donation in deliveredNotIncludedInStockDonations)
            {
                foreach (var product in donation.Products!)
                {
                    var stockItemKey = (product.ProductId, donation.WarehouseId);
                    if (allStock.TryGetValue(stockItemKey, out var stockItem))
                    {
                        stockItem.Quantity += product.Quantity;
                        donation.IsIncludedInStock = true;
                    }
                    else
                    {
                        var newStockItem = new Stock
                        {
                            ProductId = product.ProductId,
                            Quantity = product.Quantity,
                            Status = DbEntityStatus.Active,
                            UpdatedAt = DateTime.UtcNow,
                            UpdateUserId = UserSeeder.ServiceUser.Id,
                            WarehouseId = donation.WarehouseId
                        };
                        donation.IsIncludedInStock = true;
                        _appDbContext.Stocks.Add(newStockItem);
                        allStock.Add(stockItemKey, newStockItem);
                    }
                }
            }

            await _appDbContext.SaveChangesAsync();

            return new OkObjectResult(new { Message = "OK" });
        }
    }
}
