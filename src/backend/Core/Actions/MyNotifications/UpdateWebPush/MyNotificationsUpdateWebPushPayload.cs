namespace Core.Actions.MyNotifications.UpdateWebPush
{
    public class MyNotificationsUpdateWebPushPayload
    {
        public string Endpoint { get; set; } = default!;
        public string Auth { get; set; } = default!;
        public string P256dh { get; set; } = default!;
    }
}
