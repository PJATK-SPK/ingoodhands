using Core.Auth0;
using Core.Database;
using Core.Database.Models;
using Core.Exceptions;
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
        private readonly AppDbContext _appDbContext;


        public PostLoginAction(ICurrentUserService currentUserService, UserDataValidationService userDataValidationService, AppDbContext appDbContext, UserCreationService userService)
        {
            _currentUserService = currentUserService;
            _userDataValidationService = userDataValidationService;
            _appDbContext = appDbContext;
            _userService = userService;
        }

        public async Task<ActionResult> Execute()
        {
            var auth0UserInfo = await _currentUserService.GetUserInfo();
            var isUserInfoValid = _userDataValidationService.Check(auth0UserInfo);

            if (!isUserInfoValid)
            {
                throw new DataCheckValidationException("CurrentAuth0UserInfo in PostLoginAction didn't pass validation");
            }

            var user = await _appDbContext.Users.SingleOrDefaultAsync(c => c.Email == auth0UserInfo.Email);
            var auth0UserFromDatabase = await _appDbContext.Auth0Users.SingleOrDefaultAsync(c => c.Identifier == auth0UserInfo.Identifier);

            var result = await _userService.CreateUserAndAddToDatabase(user, auth0UserFromDatabase, auth0UserInfo);

            return new OkObjectResult(true);
        }
    }
}
