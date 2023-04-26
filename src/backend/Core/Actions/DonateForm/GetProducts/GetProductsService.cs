using Core.Database;
using Core.Database.Enums;
using Core.Exceptions;
using Core.Services;
using HashidsNet;
using Microsoft.Extensions.Logging;
using System.Linq.Dynamic.Core;
using Z.EntityFramework.Plus;

namespace Core.Actions.DonateForm.GetProducts
{
    public class GetProductsService
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<GetProductsService> _logger;
        private readonly Hashids _hashids;
        private readonly RoleService _roleService;

        public GetProductsService(AppDbContext appDbContext, ILogger<GetProductsService> logger, Hashids hashids, RoleService roleService)
        {
            _appDbContext = appDbContext;
            _logger = logger;
            _hashids = hashids;
            _roleService = roleService;
        }

        public async Task<List<GetProductsResponse>> GetProducts(RoleName? roleName)
        {
            if (roleName != null)
            {
                await _roleService.ThrowIfNoRole((RoleName)roleName);
            }

            var listOfProducts = await _appDbContext.Products
                .Where(c => c.Status == DbEntityStatus.Active)
                .FromCache()
                .OrderBy(c => c.Name)
                .ToDynamicListAsync();

            if (!listOfProducts.Any())
            {
                _logger.LogError("Couldn't find any active products in database");
                throw new ApplicationErrorException("Sorry there seems to be a problem with our service");
            }

            var response = listOfProducts.Select(c => new GetProductsResponse
            {
                Id = _hashids.EncodeLong(c.Id),
                Name = c.Name,
                Unit = c.Unit.ToString().ToLower(),
            }).ToList();

            return response;
        }
    }
}
