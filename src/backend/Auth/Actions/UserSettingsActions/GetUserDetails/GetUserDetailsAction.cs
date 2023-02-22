using Auth.Models;
using Auth.Services;
using Core.Setup.Auth0;
using Core.Services;
using FluentValidation;
using HashidsNet;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Actions.UserSettingsActions.GetUserDetails
{
    public class GetUserDetailsAction
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly GetCurrentUserService _getCurrentUserService;
        private readonly CurrentUserInfoValidator _validator;
        private readonly Hashids _hashids;

        public GetUserDetailsAction(
            ICurrentUserService currentUserService,
            GetCurrentUserService getCurrentUserService,
            CurrentUserInfoValidator validator,
            Hashids hashids)
        {
            _currentUserService = currentUserService;
            _getCurrentUserService = getCurrentUserService;
            _validator = validator;
            _hashids = hashids;
        }

        public async Task<OkObjectResult> Execute()
        {
            var auth0UserInfo = await _currentUserService.GetUserInfo();
            _validator.ValidateAndThrow(auth0UserInfo);

            var currentUser = await _getCurrentUserService.Execute(auth0UserInfo);

            var result = new UserDetailsResponse(currentUser, _hashids);

            return new OkObjectResult(result);
        }
    }
}
