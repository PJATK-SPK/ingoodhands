using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Orders.Actions.RequestHelpActions.RequestHelpGetMap;

namespace WebApi.Controllers.Order;

[EnableCors]
[ApiController]
[Route("request-help")]
public class RequestHelpController : ControllerBase
{
    private readonly RequestHelpGetMapAction _requestHelpGetMapAction;

    public RequestHelpController(RequestHelpGetMapAction requestHelpGetMapAction)
    {
        _requestHelpGetMapAction = requestHelpGetMapAction;
    }

    [HttpGet("map")]
    public async Task<ActionResult> GetMap() => await _requestHelpGetMapAction.Execute();
}
