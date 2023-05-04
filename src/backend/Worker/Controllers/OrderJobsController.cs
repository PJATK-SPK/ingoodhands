using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Orders.Jobs.CreateDeliveries;
using Orders.Jobs.RecalcOrdersPercentage;

namespace Worker.Controllers
{
    [EnableCors]
    [ApiController]
    [Route("order-jobs")]
    public class OrderJobsController : ControllerBase
    {
        private readonly CreateDeliveriesJob _createDeliveriesJob;
        private readonly RecalcOrdersPercentageJob _recalcOrdersPercentageJob;

        public OrderJobsController(CreateDeliveriesJob createDeliveriesJob, RecalcOrdersPercentageJob recalcOrdersPercentageJob)
        {
            _createDeliveriesJob = createDeliveriesJob;
            _recalcOrdersPercentageJob = recalcOrdersPercentageJob;
        }

        [HttpPost("create-deliveries")]
        public Task<ActionResult> SetExpiredDonations() => _createDeliveriesJob.Execute();

        [HttpPost("recalc-percentages")]
        public Task<ActionResult> RecalcOrderPercentages() => _recalcOrdersPercentageJob.Execute();
    }
}
