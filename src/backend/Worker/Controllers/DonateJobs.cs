using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Worker.Controllers
{
    [EnableCors]
    [ApiController]
    [Route("donate-jobs")]
    public class DonateJobs : ControllerBase
    {
        [HttpPost("do-set-expired-donations")]
        public async Task<ActionResult> SetExpiredDonations()
        {
            return await Task.FromResult(Ok());
        }
    }
}
