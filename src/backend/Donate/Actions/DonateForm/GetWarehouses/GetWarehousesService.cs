using Core.Database;
using Core.Exceptions;
using Core.Services;
using HashidsNet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Dynamic.Core;
using Z.EntityFramework.Plus;

namespace Donate.Actions.DonateForm.GetWarehouses
{
    public class GetWarehousesService
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<GetWarehousesService> _logger;
        private readonly Hashids _hashids;

        public GetWarehousesService(AppDbContext appDbContext, ILogger<GetWarehousesService> logger, Hashids hashids)
        {
            _appDbContext = appDbContext;
            _logger = logger;
            _hashids = hashids;
        }

        public async Task<List<GetWarehousesResponse>> GetActiveWarehouses()
        {
            var listOfWarehouses = await _appDbContext.Warehouses
                .Include(c => c.Address).ThenInclude(c => c.Country)
                .Where(c => c.Status == Core.Database.Enums.DbEntityStatus.Active).FromCache().ToDynamicListAsync();

            if (listOfWarehouses.Count == 0)
            {
                _logger.LogError("Coudln't find any active warehouses in database");
                throw new ApplicationErrorException("Sorry there seems to be a problem with our service");
            }

            var response = listOfWarehouses.Select(c => new GetWarehousesResponse
            {
                Id = _hashids.EncodeLong(c.Id),
                Name = c.ShortName,
                CountryName = c.Address!.Country!.EnglishName,
                GpsLatitude = c.Address.GpsLatitude,
                GpsLongitude = c.Address.GpsLongitude,
                City = c.Address.City,
                PostalCode = c.Address.PostalCode,
                Street = StreetFullNameBuilderService.Build(
                    c.Address.Street,
                    c.Address.StreetNumber,
                    c.Address.Apartment
                    )
            }).ToList();

            return response;
        }
    }
}
