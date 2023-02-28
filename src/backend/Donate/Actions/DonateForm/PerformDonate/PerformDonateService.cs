using Core.Database;
using Core.Database.Enums;
using Core.Database.Models.Core;
using Core.Exceptions;
using Core.Services;
using Core.Setup.Auth0;
using Donate.Actions.DonateForm.GetProducts;
using Donate.Actions.DonateForm.GetWarehouses;
using Donate.Services.DonateNameBuilder;
using HashidsNet;
using Microsoft.Extensions.Logging;
using Z.EntityFramework.Plus;
using System.Linq.Dynamic.Core;
using Core.Database.Seeders;
using Donate.Shared;

namespace Donate.Actions.DonateForm.PerformDonate
{
    public class PerformDonateService
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<GetWarehousesService> _logger;
        private readonly ICurrentUserService _currentUserService;
        private readonly GetCurrentUserService _getCurrentUserService;
        private readonly DonateNameBuilderService _donateNameBuilderService;
        private readonly RoleService _roleService;
        private readonly CounterService _counterService;
        private readonly Hashids _hashids;

        public PerformDonateService(
            AppDbContext appDbContext,
            ILogger<GetWarehousesService> logger,
            ICurrentUserService currentUserService,
            GetCurrentUserService getCurrentUserService,
            DonateNameBuilderService donateNameBuilderService,
            RoleService roleService,
            CounterService counterService,
            Hashids hashids
            )
        {
            _appDbContext = appDbContext;
            _logger = logger;
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

            var listOfProducts = await _appDbContext.Products.Where(c => c.Status == DbEntityStatus.Active).FromCache().ToDynamicListAsync();

            if (!listOfProducts.Any())
            {
                _logger.LogError("Couldn't find any active products in database");
                throw new ApplicationErrorException("Sorry there seems to be a problem with our service");
            }

            var listOfDonationProducts = payload.Products.Select(c => new DonationProduct
            {
                ProductId = _hashids.DecodeSingleLong(c.Id),
                Quantity = c.Quantity,
                UpdateUserId = UserSeeder.ServiceUser.Id,
                UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
                Status = DbEntityStatus.Active
            }).ToList();

            var nextCounterId = _counterService.GetAndUpdateNextCounter(TableName.Donations);
            var donateName = _donateNameBuilderService.Build(nextCounterId.Result);

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
                UpdateUserId = UserSeeder.ServiceUser.Id,
                UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
                Status = DbEntityStatus.Active
            };

            _appDbContext.Add(newDonation);
            _appDbContext.SaveChanges();

            var response = new PerformDonateResponse
            {
                DonateName = donateName
            };

            return response;
        }
    }
}
