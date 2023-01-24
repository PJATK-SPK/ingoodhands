using AuthService.BusinessLogic.PostLogin;
using AuthService.BusinessLogic.UserSettings;
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
        private readonly UserSettingsAction _userSettingsAction;

        public UserSettingsController(UserSettingsAction userSettingsAction)
        {
            _userSettingsAction = userSettingsAction;
        }
        [Authorize]
        [HttpGet("auth0-users")]
        public async Task<ActionResult> GetAuth0Users() => await _userSettingsAction.Execute();
    }
}
