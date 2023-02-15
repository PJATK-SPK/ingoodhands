using Auth.Services;
using Core.Exceptions;
using Core.Setup.Auth0;
using FluentValidation;
using HashidsNet;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Actions.AuthActions.PostLogin
{
    public class PostLoginAction
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly CurrentUserInfoValidator _validator;
        private readonly PostLoginUserCreationService _userService;
        private readonly Hashids _hashids;

        public PostLoginAction(
            ICurrentUserService currentUserService,
            CurrentUserInfoValidator validator,
            PostLoginUserCreationService userService,
            Hashids hashids)
        {
            _currentUserService = currentUserService;
            _validator = validator;
            _userService = userService;
            _hashids = hashids;
        }

        public async Task<ActionResult> Execute()
        {
            var auth0UserInfo = await _currentUserService.GetUserInfo();
            _validator.ValidateAndThrow(auth0UserInfo);

            var user = await _userService.CreateUserAndAddToDatabase(auth0UserInfo);

            var result = new PostLoginActionResponse
            {
                Id = _hashids.EncodeLong(user.Id),
                FirstName = user.FirstName,
                LastName = user.LastName,
            };

            return new OkObjectResult(result);
        }
    }
}
