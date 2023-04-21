using Core.Database;
using Microsoft.AspNetCore.Mvc;

namespace Orders.Jobs.RecalcOrdersPercentage
{
    public class RecalcOrdersPercentageJob
    {
        private readonly AppDbContext _appDbContext;
        private readonly RecalcOrdersPercentageJobDataService _recalcOrdersPercentageJobDataService;


        public RecalcOrdersPercentageJob(
            RecalcOrdersPercentageJobDataService recalcOrdersPercentageJobDataService,
            AppDbContext appDbContext)
        {
            _recalcOrdersPercentageJobDataService = recalcOrdersPercentageJobDataService;
            _appDbContext = appDbContext;
        }

        public async Task<ActionResult> Execute()
        {
            var listOfOrders = await _recalcOrdersPercentageJobDataService.Fetch();


            foreach (var order in listOfOrders)
            {
                RecalcOrdersPercentageJobService.CalculateAndUpdatePercentage(order);
            }

            await _appDbContext.SaveChangesAsync();

            return new OkObjectResult(new { Message = "OK" });
        }
    }
}
