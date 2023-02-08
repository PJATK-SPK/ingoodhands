using Auth.Actions.AuthActions.PostLogin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Auth
{
    [EnableCors]
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly PostLoginAction _postLoginAction;

        public AuthController(PostLoginAction postLoginAction)
        {
            _postLoginAction = postLoginAction;
        }

        [Authorize]
        [HttpGet("postlogin")]
        public async Task<ActionResult> PostLogin() => await _postLoginAction.Execute();
    }
}
