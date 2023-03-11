using Core.Database;
using HashidsNet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Core.Actions.MyNotifications.GetList
{
    public class MyNotificationsGetListLast30DaysAction
    {
        private readonly AppDbContext _appDbContext;
        private readonly Hashids _hashids;

        public MyNotificationsGetListLast30DaysAction(AppDbContext appDbContext, Hashids hashids)
        {
            _appDbContext = appDbContext;
            _hashids = hashids;
        }

        public async Task<OkObjectResult> Execute()
        {
            var dbResult = await _appDbContext.Notifications
                .Where(c => c.CreationDate > DateTime.UtcNow.AddDays(-30))
                .OrderByDescending(c => c.CreationDate)
                .ToListAsync();

            var response = dbResult.Select(c => new MyNotificationsGetListLast30DaysResponseItem()
            {
                Id = _hashids.EncodeLong(c.Id),
                CreationDate = c.CreationDate,
                Message = c.Message
            }).ToList();

            return new OkObjectResult(response);
        }
    }
}

