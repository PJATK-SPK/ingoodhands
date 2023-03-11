using Core.Database;
using Core.Database.Enums;
using Core.Exceptions;
using Core.Services;
using Core.Setup.Auth0;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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

        public async Task Pickup(string donationName)
        {
            var auth0UserInfo = _currentUserService.GetUserInfo();
            var currentUser = await _getCurrentUserService.Execute(auth0UserInfo.Result);

            await _roleService.ThrowIfNoRole(RoleName.WarehouseKeeper);

            var userHasAssignedWarehouseId = await _appDbContext.Users
                .Where(u => u.Id == currentUser.Id && u.WarehouseId != null)
                .AnyAsync();

            if (!userHasAssignedWarehouseId)
            {
                _logger.LogError("User {id} doesn't have warehouse assigned, warehouseId is empty", currentUser.Id);
                throw new ApplicationErrorException("You do not have an assigned warehouse. Contact Administrator to assign warehouse to you");
            }

            var donation = await _appDbContext.Donations.SingleOrDefaultAsync(c => c.Name == donationName);

            if (donation == null)
            {
                _logger.LogError("Donation by it's DNT \"{name}\" number has not been found", donationName);
                throw new ItemNotFoundException("Donation not found");
            }

            if (donation!.IsDelivered)
            {
                _logger.LogError("Donation found by id:{id} is already delivered", donation.Id);
                throw new ClientInputErrorException("We are sorry, but this donation has been already delievered");
            }

            donation.IsDelivered = true;
            await _appDbContext.SaveChangesAsync();
            await _notificationService.AddAsync(currentUser.Id, $"Your donation {donationName} has arrived at the warehouse!");
        }
    }
}
