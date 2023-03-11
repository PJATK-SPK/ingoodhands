using Core.Database;
using Core.Database.Enums;
using Core.Database.Models.Core;
using Core.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Orders.Actions.CreateOrderActions.CreateOrderGetCountries
{
    public class CreateOrderGetCountriesService
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<CreateOrderGetCountriesService> _logger;

        public CreateOrderGetCountriesService(AppDbContext appDbContext, ILogger<CreateOrderGetCountriesService> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public async Task<List<string>> GetActiveCountries()
        {
            var listOfCountriesNames = await _appDbContext.Countries
                .Where(c => c.Status == DbEntityStatus.Active)
                .Select(c => c.EnglishName)
                .ToListAsync();

            if (!listOfCountriesNames.Any())
            {
                _logger.LogError("Couldn't find any active Countries in database");
                throw new ItemNotFoundException("Couldn't load list of countries. Please retry. If problems persists, please contact administrator.");
            }

            return listOfCountriesNames;
        }
    }
}
