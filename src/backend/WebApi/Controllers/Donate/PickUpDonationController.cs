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
    [HttpGet("{warehouses}")]
    public async Task<ActionResult> GetWarehouses()
    {
        var result = new List<string>
        {
            "PL001",
            "PL002",
            "PL003",
        };

        return await Task.FromResult(Ok(result));
    }

    [HttpPost("{donationNumber}")]
    public async Task<ActionResult> PickUp(string donationNumber, string warehouseNumber)
    {
        return await Task.FromResult(Ok(new { Message = "OK" }));
    }
}
