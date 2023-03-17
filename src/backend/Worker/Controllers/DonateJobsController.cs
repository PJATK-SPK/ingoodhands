using Donate.Jobs.IncludeToStock;
using Donate.Jobs.SetExpiredDonations;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Worker.Controllers
{
    [EnableCors]
    [ApiController]
    [Route("donate-jobs")]
    public class DonateJobsController : ControllerBase
    {
        private readonly SetExpiredDonationsJob _setExpiredDonationsJob;
        private readonly IncludeToStockJob _includeToStockJob;

        public DonateJobsController(SetExpiredDonationsJob setExpiredDonationsJob, IncludeToStockJob includeToStockJob)
        {
            _setExpiredDonationsJob = setExpiredDonationsJob;
            _includeToStockJob = includeToStockJob;
        }

        [HttpPost("set-expired-donations")]
        public Task<ActionResult> SetExpiredDonations() => _setExpiredDonationsJob.Execute();

        [HttpPost("include-to-stock")]
        public Task<ActionResult> IncludeToStock() => _includeToStockJob.Execute();
    }
}
