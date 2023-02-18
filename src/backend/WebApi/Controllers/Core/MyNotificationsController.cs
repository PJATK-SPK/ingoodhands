using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Auth;

[EnableCors]
[ApiController]
[Authorize]
[Route("my-notifications")]
public class MyNotificationsController : ControllerBase
{
    public class DeleteMeMyNotificationsResponseItem
    {
        public DateTime CreationDate { get; set; } = default!;
        public string Message { get; set; } = default!;
    }
    [HttpGet]
    public async Task<ActionResult> GetForLatest30Days()
    {
        // sortujemy od najnowszego!

        var result = new List<DeleteMeMyNotificationsResponseItem>
        {
            new DeleteMeMyNotificationsResponseItem
            {
                CreationDate= DateTime.UtcNow.AddDays(-14),
                Message="Your donation DNT1 was delivered to warehouse PL001!"
            },
             new DeleteMeMyNotificationsResponseItem
            {
                CreationDate= DateTime.UtcNow.AddDays(-5),
                Message="Your donation DNT2 was delivered to warehouse PL002!"
            },
        };

        return await Task.FromResult(Ok(result));
    }
}
