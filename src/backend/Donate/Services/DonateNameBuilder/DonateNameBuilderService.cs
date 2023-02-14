using Core.Exceptions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donate.Services.DonateNameBuilder
{
    public class DonateNameBuilderService
    {
        private readonly ILogger<DonateNameBuilderService> _logger;

        public DonateNameBuilderService(ILogger<DonateNameBuilderService> logger)
        {
            _logger = logger;
        }

        public async Task<string> DonateNameBuilder(long id)
        {
            if (id >= 1000000 || id < 1)
            {
                _logger.LogError("Id is over 1000000, didn't pass if valdiation");
                throw new HttpError500Exception("Donation id is out of range");
            }

            var donateName = "DNT" + id.ToString("D6");
            return await Task.FromResult(donateName);
        }
    }
}
