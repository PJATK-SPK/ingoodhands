using Core.Database;
using Core.Database.Enums;
using Core.Services;
using Core.Setup.Auth0;
using HashidsNet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Orders.Actions.StocksActions.StocksGetList
{
    public class StocksGetListService
    {
        private readonly AppDbContext _appDbContext;
        private readonly Hashids _hashids;
        private readonly RoleService _roleService;
        private readonly ICurrentUserService _currentUserService;
        private readonly GetCurrentUserService _getCurrentUserService;

        public StocksGetListService(
            AppDbContext appDbContext,
            Hashids hashids,
            RoleService roleService,
            ICurrentUserService currentUserService,
            GetCurrentUserService getCurrentUserService)
        {
            _appDbContext = appDbContext;
            _hashids = hashids;
            _roleService = roleService;
            _currentUserService = currentUserService;
            _getCurrentUserService = getCurrentUserService;
        }

        public async Task<OkObjectResult> GetStockList(int page, int pageSize)
        {
            var auth0UserInfo = await _currentUserService.GetUserInfo();
            var currentUser = _getCurrentUserService.Execute(auth0UserInfo).Result;
            await _roleService.ThrowIfNoRole(RoleName.WarehouseKeeper, currentUser.Id);

            if (currentUser.WarehouseId == null)
            {
                return new OkObjectResult(new PagedResult<StocksGetListItemResponse>()
                {
                    CurrentPage = 1,
                    PageCount = 1,
                    PageSize = 1,
                    Queryable = Array.Empty<StocksGetListItemResponse>().AsQueryable(),
                    RowCount = 0
                });
            }

            var listOfActiveStock = _appDbContext.Stocks
               .Include(c => c.Product)
               .Where(c => c.Quantity != 0 && c.Status == DbEntityStatus.Active && c.WarehouseId == currentUser.WarehouseId)
               .PageResult(page, pageSize);

            var mapped = listOfActiveStock.Queryable.Select(c => new StocksGetListItemResponse
            {
                ProductId = _hashids.EncodeLong(c.ProductId),
                ProductName = c.Product!.Name,
                Quantity = c.Quantity,
                Unit = c.Product!.Unit.ToString().ToLower(),
                WarehouseId = _hashids.EncodeLong(c.WarehouseId)
            }).ToList();

            var response = new PagedResult<StocksGetListItemResponse>()
            {
                CurrentPage = listOfActiveStock.CurrentPage,
                PageCount = listOfActiveStock.PageCount,
                PageSize = listOfActiveStock.PageSize,
                Queryable = mapped.AsQueryable(),
                RowCount = listOfActiveStock.RowCount
            };

            return new OkObjectResult(response);
        }
    }
}
