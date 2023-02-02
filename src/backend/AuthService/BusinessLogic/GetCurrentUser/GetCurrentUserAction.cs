using AuthService.BusinessLogic.GetAuth0UsersByCurrentUser;
using AuthService.Services;
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

        public GetCurrentUserAction(
            ICurrentUserService currentUserService,
            GetCurrentUserService getCurrentUserService,
            UserDataValidationService userDataValidationService
            )
        {
            _currentUserService = currentUserService;
            _getCurrentUserService = getCurrentUserService;
            _userDataValidationService = userDataValidationService;
        }

        public async Task<OkObjectResult> Execute()
        {
            var auth0UserInfo = await _currentUserService.GetUserInfo();
            _userDataValidationService.Check(auth0UserInfo);

            var currentUser = await _getCurrentUserService.GetCurrentUser(auth0UserInfo);

            return new OkObjectResult(currentUser);
        }
    }
}
