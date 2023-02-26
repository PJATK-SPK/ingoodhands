using Core.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donate.Actions.MyDonations.GetNotDeliveredCount
{
    public class GetNotDeliveredCountAction
    {
        private readonly AppDbContext _appDbContext;

        public GetNotDeliveredCountAction(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<OkObjectResult> Execute()
        {
            var numberOfNotDeliveredDonations = await _appDbContext.Donations
                .Where(c => c.IsDelivered == false && c.IsExpired == false)
                .CountAsync();

            return new OkObjectResult(numberOfNotDeliveredDonations);
        }
    }
}
