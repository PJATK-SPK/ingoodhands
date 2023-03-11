using Microsoft.AspNetCore.Mvc;

namespace Donate.Actions.PickUpDonation.PostPickUpDonation
{
    public class PostPickupDonationAction
    {
        private readonly PostPickupDonationService _postPickupDonationService;

        public PostPickupDonationAction(PostPickupDonationService postPickupDonationService)
        {
            _postPickupDonationService = postPickupDonationService;
        }

        public async Task<ActionResult> Execute(string donationName)
        {
            await _postPickupDonationService.Pickup(donationName);

            return new OkObjectResult(new { Status = "OK" });
        }
    }
}
