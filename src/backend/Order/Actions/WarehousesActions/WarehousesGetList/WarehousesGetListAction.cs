using Core.Database;
using Core.Exceptions;
using Core.Services;
using HashidsNet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Order.Actions.WarehousesActions.GetWarehouses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace Order.Actions.WarehousesActions.GetWarehousesList
{
    public class WarehousesGetListAction
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<WarehousesGetListAction> _logger;
        private readonly Hashids _hashids;

        public WarehousesGetListAction(AppDbContext appDbContext, ILogger<WarehousesGetListAction> logger, Hashids hashids)
        {
            _appDbContext = appDbContext;
            _logger = logger;
            _hashids = hashids;
        }

        public async Task<List<WarehousesGetListResponse>> Execute()
        {
            var listOfWarehouses = await _appDbContext.Warehouses
               .Where(c => c.Status == Core.Database.Enums.DbEntityStatus.Active).FromCache().ToDynamicListAsync();

            if (!listOfWarehouses.Any())
            {
                _logger.LogError("Couldn't find any active warehouses in database");
                throw new ItemNotFoundException("Sorry there seems to be a problem with our service");
            }

            var response = listOfWarehouses.Select(c => new WarehousesGetListResponse
            {
                Id = _hashids.EncodeLong(c.Id),
                Name = c.ShortName
            }).ToList();

            return response;
        }
    }
}