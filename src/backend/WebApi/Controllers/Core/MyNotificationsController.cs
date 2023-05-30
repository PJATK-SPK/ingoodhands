using Core.Actions.MyNotifications.GetList;
using Core.Actions.MyNotifications.TestWebPush;
using Core.Actions.MyNotifications.UpdateWebPush;
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
    private readonly MyNotificationsUpdateWebPushAction _myNotificationsUpdateWebPushAction;
    private readonly MyNotificationsTestWebPushAction _myNotificationsTestWebPushAction;

    public MyNotificationsController(
        MyNotificationsGetListLast30DaysAction myNotificationsGetListLast30DaysAction,
        MyNotificationsUpdateWebPushAction myNotificationsUpdateWebPushAction,
        MyNotificationsTestWebPushAction myNotificationsTestWebPushAction)
    {
        _myNotificationsGetListLast30DaysAction = myNotificationsGetListLast30DaysAction;
        _myNotificationsUpdateWebPushAction = myNotificationsUpdateWebPushAction;
        _myNotificationsTestWebPushAction = myNotificationsTestWebPushAction;
    }

    [HttpGet]
    public async Task<ActionResult> GetForLatest30Days() => await _myNotificationsGetListLast30DaysAction.Execute();

    [HttpPost("update-web-push")]
    public async Task<ActionResult> UpdateWebPush([FromBody] MyNotificationsUpdateWebPushPayload payload)
        => await _myNotificationsUpdateWebPushAction.Execute(payload);

    [HttpGet("test-web-push")]
    public async Task<ActionResult> TestWebPush()
        => await _myNotificationsTestWebPushAction.Execute();
}
