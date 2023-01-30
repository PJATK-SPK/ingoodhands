using AuthService.BusinessLogic.PostLogin;
using AuthService.BusinessLogic.UserSettings;
using Core.Database.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [EnableCors]
    [ApiController]
    [Route("user-settings")]
    public class UserSettingsController : ControllerBase
    {
        private readonly GetAuth0UsersByCurrentUserAction _getAuth0UsersByCurrentUserAction;

        public UserSettingsController(GetAuth0UsersByCurrentUserAction getAuth0UsersByCurrentUserAction)
        {
            _getAuth0UsersByCurrentUserAction = getAuth0UsersByCurrentUserAction;
        }
        [Authorize]
        [HttpGet("auth0-users")]
        public async Task<ActionResult> GetAuth0Users() => await _getAuth0UsersByCurrentUserAction.Execute();
    }
}
