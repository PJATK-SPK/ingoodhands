using Core.Database;
using Core.Exceptions;
using Donate.Actions.DonateForm.GetWarehouses;
using HashidsNet;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace Donate.Actions.DonateForm.GetProducts
{
    public class GetProductsService
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<GetWarehousesService> _logger;
        private readonly Hashids _hashids;

        public GetProductsService(AppDbContext appDbContext, ILogger<GetWarehousesService> logger, Hashids hashids)
        {
            _appDbContext = appDbContext;
            _logger = logger;
            _hashids = hashids;
        }

        public async Task<List<GetProductsResponse>> GetProducts()
        {
            var listOfProducts = await _appDbContext.Products.Where(c => c.Status == Core.Database.Enums.DbEntityStatus.Active).FromCache().ToDynamicListAsync();

            if (!listOfProducts.Any())
            {
                _logger.LogError("Couldn't find any active products in database");
                throw new ApplicationErrorException("Sorry there seems to be a problem with our service");
            }

            var response = listOfProducts.Select(c => new GetProductsResponse
            {
                Id = _hashids.EncodeLong(c.Id),
                Name = c.Name,
                Unit = c.Unit.ToString(),
            }).ToList();

            return response;
        }
    }
}
