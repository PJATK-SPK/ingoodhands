using AuthService.BusinessLogic.PatchUserDetails;
using AuthService.BusinessLogic.PostLogin;
using AuthService.BusinessLogic.GetAuth0UsersByCurrentUser;
using Core.Database.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

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

        public UserSettingsController(GetAuth0UsersByCurrentUserAction getAuth0UsersByCurrentUserAction, PatchUserDetailsAction updateUserDetails)
        {
            _getAuth0UsersByCurrentUserAction = getAuth0UsersByCurrentUserAction;
            _patchUserDetailsAction = updateUserDetails;
        }

        [HttpGet("auth0-users")]
        public async Task<ActionResult> GetAuth0Users() => await _getAuth0UsersByCurrentUserAction.Execute();

        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch([FromBody] PatchUserDetailsPayload userSettingsPayload, long id) => await _patchUserDetailsAction.Execute(userSettingsPayload, id);
    }
}
