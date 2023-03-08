using Core.Database;
using Core.Database.Enums;
using Core.Database.Models.Core;
using Core.Database.Seeders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var deliveredNotIncludedInStockDonations = await _appDbContext.Donations.Where(c => c.IsDelivered & !c.IsIncludedInStock).ToListAsync();
            var allStock = await _appDbContext.Stocks.ToDictionaryAsync(c => c.ProductId);

            foreach (var donation in deliveredNotIncludedInStockDonations)
            {
                foreach (var product in donation.Products!)
                {
                    if (allStock.TryGetValue(product.ProductId, out var stockItem))
                    {
                        stockItem.Quantity += product.Quantity;
                    }
                    else
                    {
                        var newStockItem = new Stock
                        {
                            ProductId = product.ProductId,
                            Quantity = product.Quantity,
                            Status = DbEntityStatus.Active,
                            UpdatedAt = DateTime.UtcNow,
                            UpdateUserId = UserSeeder.ServiceUser.Id
                        };
                        _appDbContext.Stocks.Add(newStockItem);
                        allStock.Add(newStockItem.ProductId, newStockItem);
                    }
                }
            }

            await _appDbContext.SaveChangesAsync();

            return new OkObjectResult(new { Message = "OK" });
        }
    }
}
