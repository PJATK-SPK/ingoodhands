using AuthService.BusinessLogic.PostLogin;
using Core.Auth0;
using Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AuthService.BusinessLogic.GetAuth0UsersByCurrentUser
{
    public class GetAuth0UsersByCurrentUserAction
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly GetAuth0UsersByCurrentUserService _getAuth0UsersByCurrentUserService;
        private readonly UserDataValidationService _userDataValidationService;
        private readonly ILogger<GetAuth0UsersByCurrentUserAction> _logger;

        public GetAuth0UsersByCurrentUserAction(
            ICurrentUserService currentUserService,
            GetAuth0UsersByCurrentUserService getAuth0UsersByCurrentUserService,
            UserDataValidationService userDataValidationService,
            ILogger<GetAuth0UsersByCurrentUserAction> logger)
        {
            _currentUserService = currentUserService;
            _getAuth0UsersByCurrentUserService = getAuth0UsersByCurrentUserService;
            _userDataValidationService = userDataValidationService;
            _logger = logger;
        }

        public async Task<OkObjectResult> Execute()
        {
            var auth0UserInfo = await _currentUserService.GetUserInfo();
            var isUserInfoValid = _userDataValidationService.Check(auth0UserInfo);

            if (!isUserInfoValid)
            {
                _logger.LogError("Auth0UserInfo in GetAuth0UsersByCurrentUserAction didn't pass validation as it has nulls");
                throw new HttpError500Exception("Your Auth0User data is invalid");
            }

            var currentUserAuth0UsersList = await _getAuth0UsersByCurrentUserService.GetAllAuth0UsersFromUser(auth0UserInfo);

            return new OkObjectResult(currentUserAuth0UsersList);
        }
    }
}
