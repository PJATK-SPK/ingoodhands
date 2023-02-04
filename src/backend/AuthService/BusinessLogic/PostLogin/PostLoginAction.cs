using AuthService.Services;
using Core.Auth0;
using Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AuthService.BusinessLogic.PostLogin
{
    public class PostLoginAction
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly UserDataValidationService _userDataValidationService;
        private readonly UserCreationService _userService;

        public PostLoginAction(
            ICurrentUserService currentUserService,
            UserDataValidationService userDataValidationService,
            UserCreationService userService
            )
        {
            _currentUserService = currentUserService;
            _userDataValidationService = userDataValidationService;
            _userService = userService;
        }

        public async Task<ActionResult> Execute()
        {
            var auth0UserInfo = await _currentUserService.GetUserInfo();
            _userDataValidationService.Check(auth0UserInfo);

            var user = await _userService.CreateUserAndAddToDatabase(auth0UserInfo);

            return new OkObjectResult(user);
        }
    }
}
