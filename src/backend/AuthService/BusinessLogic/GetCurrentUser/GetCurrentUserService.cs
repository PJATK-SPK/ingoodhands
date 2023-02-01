using AuthService.BusinessLogic.GetAuth0UsersByCurrentUser;
using Core.Auth0;
using Core.Database;
using Core.Database.Models;
using Core.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.BusinessLogic.GetCurrentUser
{
    public class GetCurrentUserService
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<GetCurrentUserService> _logger;

        public GetCurrentUserService(AppDbContext appDbContext, ILogger<GetCurrentUserService> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public async Task<User?> GetCurrentUser(CurrentUserInfo auth0UserInfo)
        {
            var userFromDatabase = await _appDbContext.Auth0Users.Where(c => c.Identifier == auth0UserInfo.Identifier).Select(c => c.User).SingleOrDefaultAsync();
            if (userFromDatabase == null)
            {
                _logger.LogError($"User with email {auth0UserInfo.Email} was not found in table Auth0Users");
                throw new HttpError500Exception("Sorry we couldn't find your user in database");
            }

            return userFromDatabase!;
        }
    }
}