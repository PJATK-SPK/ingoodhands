using Core.Database;
using Core.Database.Enums;
using Core.Database.Models.Core;
using Core.Services;
using Core.Setup.Auth0;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Core.Actions.MyNotifications.UpdateWebPush
{
    public class MyNotificationsUpdateWebPushAction
    {
        private readonly AppDbContext _appDbContext;
        private readonly ICurrentUserService _currentUserService;
        private readonly GetCurrentUserService _getCurrentUserService;

        public MyNotificationsUpdateWebPushAction(
            AppDbContext appDbContext,
            ICurrentUserService currentUserService,
            GetCurrentUserService getCurrentUserService)
        {
            _appDbContext = appDbContext;
            _currentUserService = currentUserService;
            _getCurrentUserService = getCurrentUserService;
        }

        public async Task<OkObjectResult> Execute(MyNotificationsUpdateWebPushPayload payload)
        {
            var auth0UserInfo = await _currentUserService.GetUserInfo();
            var currentUser = await _getCurrentUserService.Execute(auth0UserInfo);

            var webPush = await _appDbContext.UsersWebPush.SingleOrDefaultAsync(c => c.UserId == currentUser.Id);

            if (webPush == null)
            {
                _appDbContext.UsersWebPush.Add(new UserWebPush
                {
                    UpdateUserId = currentUser.Id,
                    UpdatedAt = DateTime.UtcNow,
                    Status = DbEntityStatus.Active,
                    UserId = currentUser.Id,
                    Endpoint = payload.Endpoint,
                    Auth = payload.Auth,
                    P256dh = payload.P256dh,
                });
            }
            else
            {
                webPush.UpdatedAt = DateTime.UtcNow;
                webPush.Endpoint = payload.Endpoint;
                webPush.Auth = payload.Auth;
                webPush.P256dh = payload.P256dh;
            }

            await _appDbContext.SaveChangesAsync();

            var response = new MyNotificationsUpdateWebPushResponse { Message = "OK" };

            return new OkObjectResult(response);
        }
    }
}

