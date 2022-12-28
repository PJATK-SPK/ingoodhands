using Core.Auth0;
using Core.Database;
using Core.Database.Models;
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
    private readonly AppDbContext _context;

    public TestController(ICurrentUserService currentUserService, AppDbContext context)
    {
        _currentUserService = currentUserService;
        _context = context;
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

    [HttpGet("db-check")]
    public List<User> DbCheck()
    {
        return _context.Users.ToList();
    }
}
