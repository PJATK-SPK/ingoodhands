using Autofac;
using Core.Setup.Autofac;

namespace Core.Actions.MyNotifications.TestWebPush
{
    public class MyNotificationsTestWebPushModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<MyNotificationsTestWebPushAction>();
        }
    }
}
