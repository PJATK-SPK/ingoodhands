using Auth.Services;
using Core.Auth0;
using HashidsNet;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Actions.AuthActions.PostLogin
{
    public class PostLoginAction
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly UserDataValidationService _userDataValidationService;
        private readonly UserCreationService _userService;
        private readonly Hashids _hashids;
        public PostLoginAction(
            ICurrentUserService currentUserService,
            UserDataValidationService userDataValidationService,
            UserCreationService userService,
            Hashids hashids)
        {
            _currentUserService = currentUserService;
            _userDataValidationService = userDataValidationService;
            _userService = userService;
            _hashids = hashids;
        }

        public async Task<ActionResult> Execute()
        {
            var auth0UserInfo = await _currentUserService.GetUserInfo();
            _userDataValidationService.Check(auth0UserInfo);

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
