using Donate.Actions.PickUpDonation.PostPickUpDonation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Donate;

[EnableCors]
[ApiController]
[Authorize]
[Route("pickup-donation")]
public class PickupDonationController : ControllerBase
{
    private readonly PostPickupDonationAction _postPickupDonationAction;

    public PickupDonationController(PostPickupDonationAction postPickupDonationAction)
    {
        _postPickupDonationAction = postPickupDonationAction;
    }

    [HttpPost("{donationName}")]
    public async Task<ActionResult> PickUp(string donationName) => await _postPickupDonationAction.Execute(donationName);
}
