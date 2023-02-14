using Core.Exceptions;
using Microsoft.Extensions.Logging;

namespace Donate.Services.DonateNameBuilder
{
    public class DonateNameBuilderService
    {
        private readonly ILogger<DonateNameBuilderService> _logger;

        public DonateNameBuilderService(ILogger<DonateNameBuilderService> logger)
        {
            _logger = logger;
        }

        public string Build(long id)
        {
            if (id >= 1000000 || id < 1)
            {
                _logger.LogError("Id in Build in DonateNameBuilderService did not pass valdiation");
                throw new HttpError500Exception("Donation id is out of range");
            }

            var donateName = "DNT" + id.ToString("D6");

            return donateName;
        }
    }
}
