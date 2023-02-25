﻿using Core.Database;
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

        public PerformDonateService(
            AppDbContext appDbContext,
            ILogger<GetWarehousesService> logger,
            ICurrentUserService currentUserService,
            GetCurrentUserService getCurrentUserService,
            DonateNameBuilderService donateNameBuilderService,
            RoleService roleService,
            CounterService counterService
            )
        {
            _appDbContext = appDbContext;
            _logger = logger;
            _currentUserService = currentUserService;
            _getCurrentUserService = getCurrentUserService;
            _donateNameBuilderService = donateNameBuilderService;
            _roleService = roleService;
            _counterService = counterService;
        }

        public async Task<PerformDonateResponse> PerformDonation(PerformDonatePayload payload)
        {
            var auth0UserInfo = await _currentUserService.GetUserInfo();
            var currentUser = await _getCurrentUserService.Execute(auth0UserInfo);
            var hasRoleDonor = _roleService.HasRole(RoleName.Donor, currentUser.Id);

            if (!hasRoleDonor.Result)
            {
                _logger.LogError("User does not have Donor Role to perform that action");
                throw new UnauthorizedException("Sorry you don't have permission to perform that action");
            }

            var listOfProducts = await _appDbContext.Products.Where(c => c.Status == DbEntityStatus.Active).FromCache().ToDynamicListAsync();

            if (!listOfProducts.Any())
            {
                _logger.LogError("Couldn't find any active products in database");
                throw new ApplicationErrorException("Sorry there seems to be a problem with our service");
            }

            var listOfDonationProducts = payload.Products.Select(c => new DonationProduct
            {
                ProductId = long.Parse(c.Id),
                Quantity = c.Quantity,
                UpdateUserId = UserSeeder.ServierUser.Id,
                UpdatedAt = new DateTime(2023, 01, 01, 0, 0, 0, DateTimeKind.Utc),
                Status = DbEntityStatus.Active
            }).ToList();

            var donateName = _donateNameBuilderService.Build(_counterService.GetAndUpdateNextCounter("Donations"));

            var newDonation = new Donation
            {
                CreationUser = currentUser,
                CreationDate = DateTime.UtcNow,
                WarehouseId = long.Parse(payload.WarehouseId),
                Name = donateName,
                IsExpired = false,
                IsDelivered = false,
                IsIncludedInStock = true,
                Products = listOfDonationProducts,
                UpdateUserId = UserSeeder.ServierUser.Id,
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
