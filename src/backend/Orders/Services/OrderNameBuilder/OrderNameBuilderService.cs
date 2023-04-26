using Core.Exceptions;
using Microsoft.Extensions.Logging;

namespace Orders.Services.OrderNameBuilder
{
    public class OrderNameBuilderService
    {
        private readonly ILogger<OrderNameBuilderService> _logger;

        public OrderNameBuilderService(ILogger<OrderNameBuilderService> logger)
        {
            _logger = logger;
        }

        public string Build(long id)
        {
            if (id >= 1000000 || id < 1)
            {
                _logger.LogError("Id:{id} in Build in OrderNameBuilderService did not pass valdiation", id);
                throw new ApplicationErrorException("Order id is out of range");
            }

            var orderName = "ORD" + id.ToString("D6");

            return orderName;
        }
    }
}

