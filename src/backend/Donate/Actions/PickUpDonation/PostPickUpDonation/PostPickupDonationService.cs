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
        private readonly NotificationService _notificationService;

        public PostPickupDonationService(
            AppDbContext appDbContext,
            RoleService roleService,
            ILogger<PostPickupDonationService> logger,
            ICurrentUserService currentUserService,
            GetCurrentUserService getCurrentUserService,
            NotificationService notificationService)
        {
            _appDbContext = appDbContext;
            _roleService = roleService;
            _logger = logger;
            _currentUserService = currentUserService;
            _getCurrentUserService = getCurrentUserService;
            _notificationService = notificationService;
        }

        public async Task PostDonation(string donationName)
        {
            var auth0UserInfo = _currentUserService.GetUserInfo();
            var currentUser = await _getCurrentUserService.Execute(auth0UserInfo.Result);

            await _roleService.ThrowIfNoRole(RoleName.WarehouseKeeper);

            var userHasAssignedWarehouseId = await _appDbContext.Users
                .Where(u => u.Id == currentUser.Id && u.WarehouseId != null)
                .AnyAsync();

            if (!userHasAssignedWarehouseId)
            {
                _logger.LogError("User doesn't have warehouse assigned, warehouseId is empty");
                throw new ApplicationErrorException("User doesn't have warehouse assigned. Contact Administrator toto assign warhouse to you");
            }

            var donation = _appDbContext.Donations.SingleOrDefaultAsync(c => c.Name == donationName);

            if (donation.Result == null)
            {
                _logger.LogError("Donation by it's DNT number has not been found");
                throw new ItemNotFoundException("Donation not found");
            }

            if (donation.Result!.IsDelivered)
            {
                _logger.LogError("Donation found by ID is already delivered");
                throw new ClientInputErrorException("We are sorry, but this donation has been already delievered");
            }

            donation.Result.IsDelivered = true;
            await _appDbContext.SaveChangesAsync();
            await _notificationService.AddAsync(donation.Result!.CreationUserId, $"Your donation {donationName} has arrived at the warehouse!");
        }
    }
}
