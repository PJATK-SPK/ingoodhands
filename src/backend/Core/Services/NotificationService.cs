using Core.Database;
using Core.Database.Enums;
using Core.Database.Models.Core;
using Core.Database.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebPush;

namespace Core.Services
{
    public class NotificationService
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<NotificationService> _logger;

        private readonly string PublicKey = "BL4zflzV0yTIsPYdfwQf_0JEVjnDGg3iS37pqJqHishDblnMa6UcMr2EgVnWNS4MOBRwzruWRiWFt6WDMURK6tE";
        private readonly string PrivateKey = "pSA9iLL4Ah1B3drgRF2ifI6dpKgfUIJBaka98ytUwIE";
        private readonly string Subject = "mailto:admin@indagoodhands.web.app";

        public NotificationService(AppDbContext appDbContext, ILogger<NotificationService> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        /// <summary> Adds notification entry to table and sends WebPush. </summary>
        /// <param name="userId">For who is this notification</param>
        /// <param name="text">Notification text</param>
        /// <param name="createdByService">Set <c>UpdateUserId</c> to provided <c>userId</c> or to service user id?</param>
        public async Task AddAsync(long userId, string text, bool createdByService = false)
        {
            var model = CreateNotificationModel(userId, text, createdByService);
            _appDbContext.Notifications.Add(model);
            await _appDbContext.SaveChangesAsync();
            await TrySendWebPush(userId, text);
        }

        private async Task TrySendWebPush(long userId, string text)
        {
            var webPush = await _appDbContext.UsersWebPush.SingleOrDefaultAsync(c => c.UserId == userId);

            if (webPush == null)
                return;

            try
            {
                var subscription = new PushSubscription(webPush.Endpoint, webPush.P256dh, webPush.Auth);
                var vapidDetails = new VapidDetails(Subject, PublicKey, PrivateKey);
                var payload = @"{""notification"":{""title"":""ingoodhands"",""body"":""" + text + @""",""icon"":""https://indagoodhandsdev.web.app/assets/icons/icon-152x152.png""}}";
                var webPushClient = new WebPushClient();
                await webPushClient.SendNotificationAsync(subscription, payload, vapidDetails);
            }
            catch (Exception exception)
            {
                _logger.LogError("Failed to send notification to user {id}. Exception: {ex}", userId, exception.Message);
            }
        }

        private static Notification CreateNotificationModel(long userId, string text, bool createdByService)
        {
            return new Notification
            {
                UserId = userId,
                CreationDate = DateTime.UtcNow,
                Message = text,
                UpdateUserId = createdByService ? UserSeeder.ServiceUser.Id : userId,
                UpdatedAt = DateTime.UtcNow,
                Status = DbEntityStatus.Active
            };
        }
    }
}
