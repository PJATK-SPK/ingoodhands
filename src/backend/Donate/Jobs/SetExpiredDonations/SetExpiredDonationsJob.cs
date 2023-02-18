using Core.Database;
using Core.Database.Enums;
using Core.Database.Seeders;
using Donate.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Donate.Jobs.SetExpiredDonations
{
    public class SetExpiredDonationsJob
    {
        private readonly AppDbContext _context;

        public SetExpiredDonationsJob(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> Execute()
        {
            var expirationDate = ExpireDateService.GetExpiredDate4Today();
            var donationsToFix = await _context.Donations.Where(c => expirationDate > c.CreationDate && !c.IsExpired && !c.IsDelivered).ToListAsync();

            foreach (var toFix in donationsToFix)
            {
                toFix.UpdateDbEntity(UserSeeder.ServierUser.Id);
                toFix.IsExpired = true;
                toFix.Status = DbEntityStatus.Inactive;
            }

            await _context.SaveChangesAsync();

            return new OkObjectResult(new { Message = "OK" });
        }
    }
}
