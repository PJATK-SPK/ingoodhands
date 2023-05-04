using Core.Database;
using Core.Services;
using Core.Setup.Auth0;
using HashidsNet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Core.Actions.MyNotifications.GetList
{
    public class MyNotificationsGetListLast30DaysAction
    {
        private readonly AppDbContext _appDbContext;
        private readonly Hashids _hashids;
        private readonly ICurrentUserService _currentUserService;
        private readonly GetCurrentUserService _getCurrentUserService;

        public MyNotificationsGetListLast30DaysAction(
            AppDbContext appDbContext,
            Hashids hashids,
            ICurrentUserService currentUserService,
            GetCurrentUserService getCurrentUserService)
        {
            _appDbContext = appDbContext;
            _hashids = hashids;
            _currentUserService = currentUserService;
            _getCurrentUserService = getCurrentUserService;
        }

        public async Task<OkObjectResult> Execute()
        {
            var auth0UserInfo = _currentUserService.GetUserInfo();
            var currentUser = await _getCurrentUserService.Execute(auth0UserInfo.Result);

            var dbResult = await _appDbContext.Notifications
                .Where(c => c.UserId == currentUser.Id && c.CreationDate > DateTime.UtcNow.AddDays(-30))
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

