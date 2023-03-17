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
    }
}
