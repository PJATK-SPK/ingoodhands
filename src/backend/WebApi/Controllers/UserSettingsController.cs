using AuthService.BusinessLogic.PatchUserDetails;
using AuthService.BusinessLogic.GetAuth0UsersByCurrentUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using AuthService.BusinessLogic.GetCurrentUser;

namespace WebApi.Controllers
{
    [Authorize]
    [EnableCors]
    [ApiController]
    [Route("user-settings")]
    public class UserSettingsController : ControllerBase
    {
        private readonly GetAuth0UsersByCurrentUserAction _getAuth0UsersByCurrentUserAction;
        private readonly PatchUserDetailsAction _patchUserDetailsAction;
        private readonly GetCurrentUserAction _getCurrentUserAction;

        public UserSettingsController(
            GetAuth0UsersByCurrentUserAction getAuth0UsersByCurrentUserAction,
            PatchUserDetailsAction updateUserDetails,
            GetCurrentUserAction getCurrentUserAction
            )
        {
            _getAuth0UsersByCurrentUserAction = getAuth0UsersByCurrentUserAction;
            _patchUserDetailsAction = updateUserDetails;
            _getCurrentUserAction = getCurrentUserAction;
        }

        [HttpGet("auth0-users")]
        public async Task<ActionResult> GetAuth0Users() => await _getAuth0UsersByCurrentUserAction.Execute();

        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch([FromBody] PatchUserDetailsPayload userSettingsPayload, long id) => await _patchUserDetailsAction.Execute(userSettingsPayload, id);

        [HttpGet("current-user")]
        public async Task<ActionResult> GetUser() => await _getCurrentUserAction.Execute();
    }
}
