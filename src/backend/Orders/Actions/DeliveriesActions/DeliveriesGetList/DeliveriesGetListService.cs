using Core.Database;
using Core.Database.Enums;
using Core.Database.Models.Core;
using Core.Database.Seeders;
using Core.Services;
using HashidsNet;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Orders.Actions.DeliveriesActions.DeliveriesGetList
{
    public class DeliveriesGetListService
    {
        private readonly AppDbContext _appDbContext;
        private readonly RoleService _roleService;
        private readonly Hashids _hashids;


        public DeliveriesGetListService(AppDbContext appDbContext, RoleService roleService, Hashids hashids)
        {
            _appDbContext = appDbContext;
            _roleService = roleService;
            _hashids = hashids;
        }

        public async Task<PagedResult<DeliveriesGetListResponseItem>> GetList(int page, int pageSize, string? filter = null)
        {
            await _roleService.ThrowIfNoRole(RoleName.WarehouseKeeper);

            IQueryable<Delivery> dbResult = _appDbContext.Deliveries
                .Include(c => c.DeliveryProducts)
                .Include(c => c.Order)
                .Where(c => c.DelivererUser!.Id != UserSeeder.ServiceUser.Id);

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
                CreationDate = DateTime.UtcNow,
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
