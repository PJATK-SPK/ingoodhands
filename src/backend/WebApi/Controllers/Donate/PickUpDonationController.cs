using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Donate;

[EnableCors]
[ApiController]
[Authorize]
[Route("pick-up-donation")]
public class PickUpDonationController : ControllerBase
{

    [HttpPost("{donationNumber}")]
    public async Task<ActionResult> PickUp(string donationNumber, string warehouseNumber)
    {
        return await Task.FromResult(Ok(new { Message = "OK" }));
    }
}
