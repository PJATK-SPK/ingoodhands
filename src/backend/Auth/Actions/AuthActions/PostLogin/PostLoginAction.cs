﻿using Auth.Actions.UserSettingsActions.GetUserDetails;
using Auth.Services;
using Core.Setup.Auth0;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Actions.AuthActions.PostLogin
{
    public class PostLoginAction
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly CurrentUserInfoValidator _validator;
        private readonly PostLoginUserCreationService _userService;
        private readonly GetUserDetailsAction _getUserDetailsAction;

        public PostLoginAction(
            ICurrentUserService currentUserService,
            CurrentUserInfoValidator validator,
            PostLoginUserCreationService userService,
            GetUserDetailsAction getUserDetailsAction)
        {
            _currentUserService = currentUserService;
            _validator = validator;
            _userService = userService;
            _getUserDetailsAction = getUserDetailsAction;
        }

        public async Task<ActionResult> Execute()
        {
            var auth0UserInfo = await _currentUserService.GetUserInfo();
            _validator.ValidateAndThrow(auth0UserInfo);

            await _userService.CreateUserAndAddToDatabase(auth0UserInfo);

            return await _getUserDetailsAction.Execute();
        }
    }
}
