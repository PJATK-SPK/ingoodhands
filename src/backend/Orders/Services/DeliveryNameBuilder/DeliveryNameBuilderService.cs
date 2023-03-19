using Core.Exceptions;
using Microsoft.Extensions.Logging;

namespace Orders.Services.DeliveryNameBuilder
{
    public class DeliveryNameBuilderService
    {
        private readonly ILogger<DeliveryNameBuilderService> _logger;

        public DeliveryNameBuilderService(ILogger<DeliveryNameBuilderService> logger)
        {
            _logger = logger;
        }

        public string Build(long id)
        {
            if (id >= 1000000 || id < 1)
            {
                _logger.LogError("Id in Build in DeliveryNameBuilderService did not pass valdiation");
                throw new ApplicationErrorException("Delivery id is out of range");
            }

            var deliveryName = "DEL" + id.ToString("D6");

            return deliveryName;
        }
    }
}
