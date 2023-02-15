using Auth.Services;
using Core.Exceptions;
using Core.Setup.Auth0;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace Auth.Actions.UserSettingsActions.GetAuth0UsersByCurrentUser
{
    public class GetAuth0UsersByCurrentUserAction
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly GetAuth0UsersByCurrentUserService _getAuth0UsersByCurrentUserService;
        private readonly CurrentUserInfoValidator _validator;

        public GetAuth0UsersByCurrentUserAction(
            ICurrentUserService currentUserService,
            GetAuth0UsersByCurrentUserService getAuth0UsersByCurrentUserService,
            CurrentUserInfoValidator validator
            )
        {
            _currentUserService = currentUserService;
            _getAuth0UsersByCurrentUserService = getAuth0UsersByCurrentUserService;
            _validator = validator;
        }

        public async Task<OkObjectResult> Execute()
        {
            var auth0UserInfo = await _currentUserService.GetUserInfo();
            _validator.ValidateAndThrow(auth0UserInfo);

            var currentUserAuth0UsersList = await _getAuth0UsersByCurrentUserService.GetAllAuth0UsersFromUser(auth0UserInfo);

            return new OkObjectResult(currentUserAuth0UsersList);
        }
    }
}
