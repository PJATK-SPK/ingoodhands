using AuthService.BusinessLogic.GetAuth0UsersByCurrentUser;
using Core.Auth0;
using Core.Exceptions;
using Microsoft.Extensions.Logging;

namespace AuthService.Services
{
    public class UserDataValidationService
    {
        private readonly ILogger<UserDataValidationService> _logger;

        public UserDataValidationService(ILogger<UserDataValidationService> logger)
        {
            _logger = logger;
        }

        public bool Check(CurrentUserInfo currentAuth0UserInfo)
        {
            var check = currentAuth0UserInfo.Email != null;
            check &= currentAuth0UserInfo.EmailVerified == true;
            check &= currentAuth0UserInfo.Identifier != null;
            check &= currentAuth0UserInfo.FamilyName != null;
            check &= currentAuth0UserInfo.GivenName != null;
            check &= currentAuth0UserInfo.Name != null;
            check &= currentAuth0UserInfo.Nickname != null;

            if (!check)
            {
                _logger.LogError("Auth0UserInfo didn't pass validation as it has nulls");
                throw new HttpError500Exception("Sorry we couldn't fetch your Auth0 data");
            }

            return check;
        }
    }
}
