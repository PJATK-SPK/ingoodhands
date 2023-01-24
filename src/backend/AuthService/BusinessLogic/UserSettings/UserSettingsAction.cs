using AuthService.BusinessLogic.Exceptions;
using AuthService.BusinessLogic.PostLogin;
using Core.Auth0;
using Core.Database;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.BusinessLogic.UserSettings
{
    public class UserSettingsAction
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly UserSettingsService _userSettingsService;
        private readonly UserDataValidationService _userDataValidationService;

        public UserSettingsAction(ICurrentUserService currentUserService, UserSettingsService userSettingsService, UserDataValidationService userDataValidationService)
        {
            _currentUserService = currentUserService;
            _userSettingsService = userSettingsService;
            _userDataValidationService = userDataValidationService;
        }

        public async Task<ActionResult> Execute()
        {
            var auth0UserInfo = await _currentUserService.GetUserInfo();
            var isUserInfoValid = _userDataValidationService.Check(auth0UserInfo);

            if (!isUserInfoValid)
            {
                throw new CurrentUserDataCheckValidationException("CurrentAuth0UserInfo in UserSettingsAction didn't pass validation");
            }

            var currentUserAuth0UsersList = await _userSettingsService.GetAllAuth0UsersFromUser(auth0UserInfo);

            return new OkObjectResult(currentUserAuth0UsersList);
        }
    }
}
