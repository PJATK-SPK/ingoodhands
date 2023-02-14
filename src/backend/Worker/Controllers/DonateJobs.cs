using Donate.Jobs.SetExpiredDonations;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Worker.Controllers
{
    [EnableCors]
    [ApiController]
    [Route("donate-jobs")]
    public class DonateJobs : ControllerBase
    {
        private readonly SetExpiredDonationsJob _setExpiredDonationsJob;

        public DonateJobs(SetExpiredDonationsJob setExpiredDonationsJob)
        {
            _setExpiredDonationsJob = setExpiredDonationsJob;
        }

        [HttpPost("set-expired-donations")]
        public Task<ActionResult> SetExpiredDonations() => _setExpiredDonationsJob.Execute();
    }
}
