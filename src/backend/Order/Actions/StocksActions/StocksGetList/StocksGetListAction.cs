﻿using Core.Database;
using Core.Database.Enums;
using Core.Services;
using HashidsNet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Z.EntityFramework.Plus;

namespace Orders.Actions.StocksActions.StocksGetList
{
    public class StocksGetListAction
    {
        private readonly AppDbContext _appDbContext;
        private readonly Hashids _hashids;
        private readonly RoleService _roleService;

        public StocksGetListAction(AppDbContext appDbContext, Hashids hashids, RoleService roleService)
        {
            _appDbContext = appDbContext;
            _hashids = hashids;
            _roleService = roleService;
        }

        public async Task<OkObjectResult> Execute(int page, int pageSize)
        {
            await _roleService.ThrowIfNoRole(RoleName.WarehouseKeeper);

            var listOfActiveStock = _appDbContext.Stocks
               .Include(c => c.Product)
               .Where(c => c.Status == DbEntityStatus.Active)
               .PageResult(page, pageSize);

            var mapped = listOfActiveStock.Queryable.Select(c => new StocksGetListItemResponse
            {
                ProductId = _hashids.EncodeLong(c.ProductId),
                ProductName = c.Product!.Name,
                Quantity = c.Quantity,
                Unit = c.Product!.Unit.ToString().ToLower()
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
