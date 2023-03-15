using Core.Database;
using Core.Database.Enums;
using Core.Database.Models.Core;
using Core.Services;
using Core.Setup.Auth0;
using Donate.Actions.DonateForm.GetWarehouses;
using Donate.Services.DonateNameBuilder;
using Donate.Shared;
using HashidsNet;
using Microsoft.Extensions.Logging;
using System.Linq.Dynamic.Core;

namespace Donate.Actions.DonateForm.PerformDonate
{
    public class PerformDonateService
    {
        private readonly AppDbContext _appDbContext;
        private readonly ICurrentUserService _currentUserService;
        private readonly GetCurrentUserService _getCurrentUserService;
        private readonly DonateNameBuilderService _donateNameBuilderService;
        private readonly RoleService _roleService;
        private readonly CounterService _counterService;
        private readonly Hashids _hashids;

        public PerformDonateService(
            AppDbContext appDbContext,
            ICurrentUserService currentUserService,
            GetCurrentUserService getCurrentUserService,
            DonateNameBuilderService donateNameBuilderService,
            RoleService roleService,
            CounterService counterService,
            Hashids hashids)
        {
            _appDbContext = appDbContext;
            _currentUserService = currentUserService;
            _getCurrentUserService = getCurrentUserService;
            _donateNameBuilderService = donateNameBuilderService;
            _roleService = roleService;
            _counterService = counterService;
            _hashids = hashids;
        }

        public async Task<PerformDonateResponse> PerformDonation(PerformDonatePayload payload)
        {
            var auth0UserInfo = await _currentUserService.GetUserInfo();
            var currentUser = await _getCurrentUserService.Execute(auth0UserInfo);
            await _roleService.ThrowIfNoRole(RoleName.Donor, currentUser.Id);

            var listOfDonationProducts = payload.Products.Select(c => new DonationProduct
            {
                ProductId = _hashids.DecodeSingleLong(c.Id),
                Quantity = c.Quantity,
                UpdateUserId = currentUser.Id,
                UpdatedAt = DateTime.UtcNow,
                Status = DbEntityStatus.Active
            }).ToList();

            var nextCounterId = await _counterService.GetAndUpdateNextCounter(TableName.Donations);
            var donateName = _donateNameBuilderService.Build(nextCounterId);

            var newDonation = new Donation
            {
                CreationUser = currentUser,
                CreationDate = DateTime.UtcNow,
                WarehouseId = _hashids.DecodeSingleLong(payload.WarehouseId),
                Name = donateName,
                ExpirationDate = ExpireDateService.GetExpiredDate4Donation(DateTime.UtcNow),
                IsExpired = false,
                IsDelivered = false,
                IsIncludedInStock = false,
                Products = listOfDonationProducts,
                UpdateUserId = currentUser.Id,
                UpdatedAt = DateTime.UtcNow,
                Status = DbEntityStatus.Active
            };

            await _appDbContext.AddAsync(newDonation);
            await _appDbContext.SaveChangesAsync();

            var response = new PerformDonateResponse
            {
                DonateName = donateName
            };

            return response;
        }
    }
}
