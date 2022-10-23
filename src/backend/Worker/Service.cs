using Core.Database;

namespace Worker
{
    public class Service : BackgroundService
    {
        private readonly ILogger<Service> _logger;

        public Service(ILogger<Service> logger, AppDbContext dbContext)
        {
            _logger = logger;

            var test = dbContext.Users.ToList();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}