using Donate.Actions.PickUpDonation.PostPickUpDonation;
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
    private readonly PostPickupDonationAction _postPickupDonationAction;

    public PickUpDonationController(PostPickupDonationAction postPickupDonationAction)
    {
        _postPickupDonationAction = postPickupDonationAction;
    }

    [HttpPost("{donationNumber}")]
    public async Task<ActionResult> PickUp(string donationNumber) => await _postPickupDonationAction.Execute(donationNumber);
}
