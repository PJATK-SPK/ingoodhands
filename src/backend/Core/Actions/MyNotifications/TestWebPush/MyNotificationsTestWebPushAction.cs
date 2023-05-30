using Core.Services;
using Core.Setup.Auth0;
using Microsoft.AspNetCore.Mvc;

namespace Core.Actions.MyNotifications.TestWebPush
{
    public class MyNotificationsTestWebPushAction
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly GetCurrentUserService _getCurrentUserService;
        private readonly NotificationService _notificationService;

        public MyNotificationsTestWebPushAction(
            ICurrentUserService currentUserService,
            GetCurrentUserService getCurrentUserService,
            NotificationService notificationService)
        {
            _currentUserService = currentUserService;
            _getCurrentUserService = getCurrentUserService;
            _notificationService = notificationService;
        }

        public async Task<OkObjectResult> Execute()
        {
            var auth0UserInfo = await _currentUserService.GetUserInfo();
            var currentUser = await _getCurrentUserService.Execute(auth0UserInfo);
            await _notificationService.AddAsync(currentUser.Id, "Test message!");
            var response = new MyNotificationsTestWebPushResponse { Message = "OK" };
            return new OkObjectResult(response);
        }
    }
}

