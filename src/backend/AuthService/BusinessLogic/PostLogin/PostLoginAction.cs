using AuthService.BusinessLogic.Exceptions;
using Core.Auth0;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AuthService.BusinessLogic.PostLogin
{
    public class PostLoginAction
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly UserDataValidationService _userDataValidationService;
        private readonly UserCreationService _userService;
        private readonly ILogger<PostLoginAction> _logger;

        public PostLoginAction(
            ICurrentUserService currentUserService,
            UserDataValidationService userDataValidationService,
            UserCreationService userService,
            ILogger<PostLoginAction> logger
            )
        {
            _currentUserService = currentUserService;
            _userDataValidationService = userDataValidationService;
            _userService = userService;
            _logger = logger;
        }

        public async Task<ActionResult> Execute()
        {
            var auth0UserInfo = await _currentUserService.GetUserInfo();
            var isUserInfoValid = _userDataValidationService.Check(auth0UserInfo);

            if (!isUserInfoValid)
            {
                _logger.LogError("Auth0UserInfo in PostLoginAction didn't pass validation as it has nulls");
                throw new InvalidAuth0DataException();
            }

            var user = await _userService.CreateUserAndAddToDatabase(auth0UserInfo);

            return new OkObjectResult(user);
        }
    }
}
