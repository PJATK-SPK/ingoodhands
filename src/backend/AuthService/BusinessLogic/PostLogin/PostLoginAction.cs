﻿using Core.Auth0;
using Core.Database;
using Core.Database.Models;
using AuthService.BusinessLogic.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.BusinessLogic.PostLogin
{
    public class PostLoginAction
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly UserDataValidationService _userDataValidationService;
        private readonly UserCreationService _userService;

        public PostLoginAction(ICurrentUserService currentUserService, UserDataValidationService userDataValidationService, UserCreationService userService)
        {
            _currentUserService = currentUserService;
            _userDataValidationService = userDataValidationService;
            _userService = userService;
        }

        public async Task<ActionResult> Execute()
        {
            var auth0UserInfo = await _currentUserService.GetUserInfo();
            var isUserInfoValid = _userDataValidationService.Check(auth0UserInfo);

            if (!isUserInfoValid)
            {
                throw new CurrentUserDataCheckValidationException("CurrentAuth0UserInfo in PostLoginAction didn't pass validation");
            }

            var user = await _userService.CreateUserAndAddToDatabase(auth0UserInfo);

            return new OkObjectResult(user);
        }
    }
}
