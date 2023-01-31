﻿using Core.Auth0;
using Core.Database;
using Core.Database.Models;
using Core.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Dynamic.Core;

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
                throw new HttpError500Exception("Something went wrong, cannot find Auth0User in database");
            }

            var auth0UserUserId = auth0UserFromDatabase!.UserId;
            var currentUserAuth0UsersList = await _appDbContext.Auth0Users.Where(c => c.UserId == auth0UserUserId).ToListAsync();

            return currentUserAuth0UsersList;
        }
    }
}