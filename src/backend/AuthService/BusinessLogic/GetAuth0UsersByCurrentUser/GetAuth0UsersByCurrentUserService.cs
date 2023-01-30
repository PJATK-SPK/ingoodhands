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
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.BusinessLogic.GetAuth0UsersByCurrentUser
{
    public class GetAuth0UsersByCurrentUserService
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<GetAuth0UsersByCurrentUserService> _logger;

        public GetAuth0UsersByCurrentUserService(AppDbContext appDbContext, ILogger<GetAuth0UsersByCurrentUserService> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public async Task<List<Auth0User>> GetAllAuth0UsersFromUser(CurrentUserInfo auth0UserInfo)
        {
            var auth0UserFromDatabase = await _appDbContext.Auth0Users.SingleOrDefaultAsync(c => c.Identifier == auth0UserInfo.Identifier);

            if (auth0UserFromDatabase == null)
            {
                _logger.LogError("Object Auth0UserFromDatabase didn't pass through, because it is null");
                throw new ArgumentNullException("Something went wrong, cannot find Auth0User in database. Please contact server administrator.");
            }

            var auth0UserUserId = auth0UserFromDatabase!.UserId;
            var currentUserAuth0UsersList = await _appDbContext.Auth0Users.Where(c => c.UserId == auth0UserUserId).ToListAsync();

            return currentUserAuth0UsersList;
        }
    }
}
