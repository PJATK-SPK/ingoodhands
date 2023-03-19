using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Orders.Jobs.CreateDeliveries;

namespace Worker.Controllers
{
    [EnableCors]
    [ApiController]
    [Route("order-jobs")]
    public class OrderJobsController : ControllerBase
    {
        private readonly CreateDeliveriesJob _createDeliveriesJob;

        public OrderJobsController(CreateDeliveriesJob createDeliveriesJob)
        {
            _createDeliveriesJob = createDeliveriesJob;
        }

        [HttpPost("create-deliveries")]
        public Task<ActionResult> SetExpiredDonations() => _createDeliveriesJob.Execute();

        [HttpPost("recalc-percentages")] // @Pawel - po zrobieniu usunąć await, async (zrobić jak wyżej) i wywalic kom
        public async Task<ActionResult> RecalcOrderPercentages() => await Task.Run(() => Ok(new { Result = "OK" }));
    }
}
