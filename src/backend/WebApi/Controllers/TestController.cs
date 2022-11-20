using Core.Auth0;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[EnableCors]
[ApiController]
[Route("test")]
public class TestController : ControllerBase
{
    private readonly ICurrentUserService _currentUserService;

    public TestController(ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
    }

    [Authorize]
    [HttpGet("user-info")]
    public async Task<CurrentUserInfo> GetUserInfo()
    {
        var email = _currentUserService.GetUserEmail();
        var identifier = _currentUserService.GetUserAuthIdentifier();
        var info = await _currentUserService.GetUserInfo();
        return info;
    }

    [HttpGet("no-auth")]
    public object NoAuthCheck()
    {
        return new { Message = "OK" };
    }
}
