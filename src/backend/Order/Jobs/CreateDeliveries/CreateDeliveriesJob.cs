using Core.Database;
using Microsoft.AspNetCore.Mvc;

namespace Orders.Jobs.CreateDeliveries
{
    public class CreateDeliveriesJob
    {
        private readonly AppDbContext _context;

        public CreateDeliveriesJob(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> Execute()
        {
            // TODO

            await _context.SaveChangesAsync();

            return new OkObjectResult(new { Message = "OK" });
        }
    }
}
