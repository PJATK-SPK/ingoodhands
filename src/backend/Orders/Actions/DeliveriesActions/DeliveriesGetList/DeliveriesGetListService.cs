using Core.Database;
using Core.Database.Enums;
using Core.Database.Models.Core;
using Core.Database.Seeders;
using Core.Services;
using Core.Setup.Auth0;
using HashidsNet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orders.Actions.StocksActions.StocksGetList;
using System.Linq.Dynamic.Core;

namespace Orders.Actions.DeliveriesActions.DeliveriesGetList
{
    public class DeliveriesGetListService
    {
        private readonly AppDbContext _appDbContext;
        private readonly RoleService _roleService;
        private readonly Hashids _hashids;
        private readonly ICurrentUserService _currentUserService;
        private readonly GetCurrentUserService _getCurrentUserService;

        public DeliveriesGetListService(
            AppDbContext appDbContext,
            RoleService roleService,
            Hashids hashids,
            ICurrentUserService currentUserService,
            GetCurrentUserService getCurrentUserService)
        {
            _appDbContext = appDbContext;
            _roleService = roleService;
            _hashids = hashids;
            _currentUserService = currentUserService;
            _getCurrentUserService = getCurrentUserService;
        }

        public async Task<PagedResult<DeliveriesGetListResponseItem>> GetList(int page, int pageSize, string? filter = null)
        {
            var auth0UserInfo = await _currentUserService.GetUserInfo();
            var currentUser = _getCurrentUserService.Execute(auth0UserInfo).Result;

            await _roleService.ThrowIfNoRole(RoleName.WarehouseKeeper, currentUser.Id);

            if (currentUser.WarehouseId == null)
            {
                return new PagedResult<DeliveriesGetListResponseItem>()
                {
                    CurrentPage = 1,
                    PageCount = 1,
                    PageSize = 1,
                    Queryable = Array.Empty<DeliveriesGetListResponseItem>().AsQueryable(),
                    RowCount = 0
                };
            }

            IQueryable<Delivery> dbResult = _appDbContext.Deliveries
                .Include(c => c.DeliveryProducts)
                .Include(c => c.Order)
                .Where(c => c.DelivererUserId != null && c.DelivererUser!.Id != UserSeeder.ServiceUser.Id && c.WarehouseId == currentUser.WarehouseId);

            if (filter != null)
            {
                dbResult = dbResult.Where(u => (u.Name).ToLower().Contains(filter.ToLower()));
            }

            var result = dbResult.PageResult(page, pageSize);

            var mapped = result.Queryable.Select(c => new DeliveriesGetListResponseItem
            {
                Id = _hashids.EncodeLong(c.Id),
                DeliveryName = c.Name,
                OrderName = c.Order!.Name,
                IsDelivered = c.IsDelivered,
                IsLost = c.IsLost,
                TripStarted = c.TripStarted,
                CreationDate = c.CreationDate,
                ProductTypesCount = c.DeliveryProducts != null ? c.DeliveryProducts.Count : 0
            }).ToList();

            var response = new PagedResult<DeliveriesGetListResponseItem>()
            {
                CurrentPage = result.CurrentPage,
                PageCount = result.PageCount,
                PageSize = result.PageSize,
                Queryable = mapped.AsQueryable(),
                RowCount = result.RowCount
            };

            return response;
        }
    }
}
