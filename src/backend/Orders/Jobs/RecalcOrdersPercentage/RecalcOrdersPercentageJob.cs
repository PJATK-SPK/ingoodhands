using Autofac.Features.ResolveAnything;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orders.Jobs.CreateDeliveries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Jobs.RecalcOrdersPercentage
{
    public class RecalcOrdersPercentageJob
    {
        private readonly RecalcOrdersPercentageJobDataService _recalcOrdersPercentageJobDataService;

        public RecalcOrdersPercentageJob(RecalcOrdersPercentageJobDataService recalcOrdersPercentageJobDataService)
        {
            _recalcOrdersPercentageJobDataService = recalcOrdersPercentageJobDataService;
        }

        public async Task<ActionResult> Execute()
        {
            var listOfOrders = await _recalcOrdersPercentageJobDataService.Fetch();
            var remainders = CreateDeliveriesJobOrderRemainderService.Execute(listOfOrders);

            foreach (var order in listOfOrders)
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
