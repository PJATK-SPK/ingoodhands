using Core.Database;
using Core.Database.Enums;
using Core.Database.Models.Core;
using Core.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Core.Services
{
    public class CounterService
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<CounterService> _logger;

        public CounterService(AppDbContext appDbContext, ILogger<CounterService> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public async Task<Counter> GetCounter(TableName tableName)
        {
            var counterOfSpecifiedTable = await _appDbContext.Counters.SingleOrDefaultAsync(c => c.Name == tableName);
            if (counterOfSpecifiedTable == null)
            {
                _logger.LogError("Cannot find specified tableName:{tableName} in Counters table", tableName.ToString());
                throw new ApplicationErrorException("Sorry there seems to be problem with our service");
            }
            return counterOfSpecifiedTable;
        }

        public async Task<long> GetAndUpdateNextCounter(TableName tableName)
        {
            var nextCounterOfSpecifiedTable = GetCounter(tableName);
            nextCounterOfSpecifiedTable.Result.Value++;

            await _appDbContext.SaveChangesAsync();

            return nextCounterOfSpecifiedTable.Result.Value;
        }
    }
}
