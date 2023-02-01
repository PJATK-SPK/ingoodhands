using AuthService.BusinessLogic.GetAuth0UsersByCurrentUser;
using AuthService.BusinessLogic.PostLogin;
using Core.Auth0;
using Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.BusinessLogic.GetCurrentUser
{
    public class GetCurrentUserAction
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly GetCurrentUserService _getCurrentUserService;
        private readonly UserDataValidationService _userDataValidationService;
        private readonly ILogger<GetCurrentUserAction> _logger;

        public GetCurrentUserAction(
            ICurrentUserService currentUserService,
            GetCurrentUserService getCurrentUserService,
            UserDataValidationService userDataValidationService,
            ILogger<GetCurrentUserAction> logger
            )
        {
            _currentUserService = currentUserService;
            _getCurrentUserService = getCurrentUserService;
            _userDataValidationService = userDataValidationService;
            _logger = logger;
        }

        public async Task<OkObjectResult> Execute()
        {
            var auth0UserInfo = await _currentUserService.GetUserInfo();
            var isUserInfoValid = _userDataValidationService.Check(auth0UserInfo);

            if (!isUserInfoValid)
            {
                _logger.LogError("Auth0UserInfo in GetCurrentUserAction didn't pass validation as it has nulls");
                throw new HttpError500Exception("Sorry we couldn't fetch your Auth0 data");
            }

            var currentUser = await _getCurrentUserService.GetCurrentUser(auth0UserInfo);

            return new OkObjectResult(currentUser);
        }
    }
}
