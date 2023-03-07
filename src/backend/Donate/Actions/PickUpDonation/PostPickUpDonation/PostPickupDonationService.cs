using Core.Database;
using Core.Database.Enums;
using Core.Database.Models.Auth;
using Core.Database.Models.Core;
using Core.Database.Seeders;
using Core.Exceptions;
using Core.Services;
using Core.Setup.Auth0;
using HashidsNet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestSharp;
using System.Net.Mail;

namespace Donate.Actions.PickUpDonation.PostPickUpDonation
{
    public class PostPickupDonationService
    {
        private readonly AppDbContext _appDbContext;
        private readonly RoleService _roleService;
        private readonly ILogger<PostPickupDonationService> _logger;
        private readonly ICurrentUserService _currentUserService;
        private readonly GetCurrentUserService _getCurrentUserService;

        public PostPickupDonationService(
            AppDbContext appDbContext,
            RoleService roleService,
            ILogger<PostPickupDonationService> logger,
            ICurrentUserService currentUserService,
            GetCurrentUserService getCurrentUserService
            )
        {
            _appDbContext = appDbContext;
            _roleService = roleService;
            _logger = logger;
            _currentUserService = currentUserService;
            _getCurrentUserService = getCurrentUserService;
        }

        public async Task PostDonation(string donationName)
        {
            var auth0UserInfo = _currentUserService.GetUserInfo();
            var currentUser = await _getCurrentUserService.Execute(auth0UserInfo.Result);

            await _roleService.ThrowIfNoRole(RoleName.WarehouseKeeper);

            var userHasAssignedWarehouseId = await _appDbContext.Users
                .Where(u => u.Id == currentUser.Id && u.WarehouseId != null)
                .Select(u => u.WarehouseId)
                .FirstOrDefaultAsync();

            if (userHasAssignedWarehouseId == null)
            {
                _logger.LogError("User doesn't have warehouse assigned, warehouseId is empty");
                throw new ApplicationErrorException("User doesn't have warehouse assigned. Contact Administrator to assign warhouse to a User");
            }

            var donation = _appDbContext.Donations.SingleOrDefaultAsync(c => c.Name == donationName);

            if (donation.Result == null)
            {
                _logger.LogError("Donation by it's DNT number has not been found");
                throw new ItemNotFoundException("Donation not found");
            }

            if (donation.Result!.IsDelivered == true)
            {
                _logger.LogError("Donation found by ID is already delivered");
                throw new ClientInputErrorException("We are sorry, but this donation has been already delievered");
            }

            var notificationForDonor = new Notification
            {
                UserId = donation.Result!.CreationUserId,
                CreationDate = DateTime.UtcNow,
                Message = "Your donation has arrived at the warhouse!",
                UpdateUserId = UserSeeder.ServiceUser.Id,
                UpdatedAt = DateTime.UtcNow,
                Status = DbEntityStatus.Active
            };

            donation.Result.IsDelivered = true;
            _appDbContext.Add(notificationForDonor);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
