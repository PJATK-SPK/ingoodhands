using Core.Auth0;
using Core.WebApi.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[EnableCors]
[ApiController]
[Route("test")]
public class TestController : ControllerBase
{
    private readonly ICurrentUserService _webApiCurrentUserService;

    public TestController(ICurrentUserService webApiCurrentUserService)
    {
        _webApiCurrentUserService = webApiCurrentUserService;
    }

    [Authorize]
    [HttpGet("user-info")]
    public async Task<CurrentUserInfo> GetUserInfo()
    {
        var email = _webApiCurrentUserService.GetUserEmail();
        var identifier = _webApiCurrentUserService.GetUserAuthIdentifier();
        var info = await _webApiCurrentUserService.GetUserInfo();
        return info;
    }

    [HttpGet("no-auth")]
    public object NoAuthCheck()
    {
        return new { Message = "OK" };
    }
}
