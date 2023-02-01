using AuthService.Services;
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
            _userDataValidationService.Check(auth0UserInfo);

            var currentUserAuth0UsersList = await _getAuth0UsersByCurrentUserService.GetAllAuth0UsersFromUser(auth0UserInfo);

            return new OkObjectResult(currentUserAuth0UsersList);
        }
    }
}
