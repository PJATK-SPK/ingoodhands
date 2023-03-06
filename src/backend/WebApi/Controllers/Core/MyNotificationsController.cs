using Core.Actions.MyNotifications;
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
    private readonly MyNotificationsGetListLast30DaysAction _myNotificationsGetListLast30DaysAction;

    public MyNotificationsController(MyNotificationsGetListLast30DaysAction myNotificationsGetListLast30DaysAction)
    {
        _myNotificationsGetListLast30DaysAction = myNotificationsGetListLast30DaysAction;
    }

    [HttpGet]
    public async Task<ActionResult> GetForLatest30Days() => await _myNotificationsGetListLast30DaysAction.Execute();
}
