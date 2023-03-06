using Core.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Actions.MyNotifications
{
    public class MyNotificationsGetListLast30DaysAction
    {
        private readonly AppDbContext _appDbContext;

        public MyNotificationsGetListLast30DaysAction(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<OkObjectResult> Execute()
        {
            var dbResult = await _appDbContext.Notifications.Where(c => c.CreationDate > (DateTime.UtcNow.AddDays(-30))).ToListAsync();

            var response = dbResult.Select(c => new MyNotificationsGetListLast30DaysResponseItem()
            {
                CreationDate = c.CreationDate,
                Message = c.Message
            });

            return new OkObjectResult(response);
        }
    }
}

