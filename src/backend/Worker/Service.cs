using Core.Database;
using Microsoft.EntityFrameworkCore;

namespace Worker
{
    public class Service : BackgroundService
    {
        private readonly ILogger<Service> _logger;
        private readonly AppDbContext _appDbContext;

        public Service(ILogger<Service> logger, AppDbContext appDbContext)
        {
            _logger = logger;
            _appDbContext = appDbContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Database users count: {count}", _appDbContext.Users.Count());

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}