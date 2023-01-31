﻿using AuthService.BusinessLogic.GetAuth0UsersByCurrentUser;
using Core.Database;
using Core.Database.Models;
using Core.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AuthService.BusinessLogic.PatchUserDetails
{
    public class PatchUserDetailsService
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<GetAuth0UsersByCurrentUserService> _logger;

        public PatchUserDetailsService(AppDbContext appDbContext, ILogger<GetAuth0UsersByCurrentUserService> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public async Task<User> PatchUserFirstNameAndLastName(PatchUserDetailsPayload userSettingsPayload, long id)
        {
            var userFromDatabase = await _appDbContext.Users.SingleOrDefaultAsync(c => c.Id == id);
            if (userFromDatabase == null)
            {
                _logger.LogError("UserFromDatabase in PatchUserDetailsService is null");
                throw new HttpError500Exception("Sorry we couldn't find your user in database");
            }

            userFromDatabase.FirstName = userSettingsPayload.FirstName;
            userFromDatabase.LastName = userSettingsPayload.LastName;
            await _appDbContext.SaveChangesAsync();

            return userFromDatabase;
        }
    }
}