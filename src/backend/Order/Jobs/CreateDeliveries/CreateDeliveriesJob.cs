﻿using Core.Database;
using Microsoft.AspNetCore.Mvc;

namespace Orders.Jobs.CreateDeliveries
{
    public class CreateDeliveriesJob
    {
        private readonly AppDbContext _context;
        private readonly CreateDeliveriesJobDataService _dataService;
        private readonly CreateDeliveriesJobWarehouseService _warehouseService;

        public CreateDeliveriesJob(
            AppDbContext context,
            CreateDeliveriesJobDataService dataService,
            CreateDeliveriesJobWarehouseService warehouseService)
        {
            _context = context;
            _dataService = dataService;
            _warehouseService = warehouseService;
        }

        public async Task<ActionResult> Execute()
        {
            var data = await _dataService.Fetch();
            var remainders = CreateDeliveriesJobOrderRemainderService.Execute(data.Orders);

            foreach (var order in data.Orders)
            {
                var found = remainders.TryGetValue(order.Id, out var remainder);
                if (!found) continue;

                await _warehouseService.AddDeliveriesToOrder(order, remainder!, data.Stocks);
            }

            await _context.SaveChangesAsync();

            return new OkObjectResult(new { Message = "OK" });
        }
    }
}