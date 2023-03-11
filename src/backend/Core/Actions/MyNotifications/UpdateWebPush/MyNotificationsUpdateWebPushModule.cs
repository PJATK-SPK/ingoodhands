using Autofac;
using Core.Setup.Autofac;

namespace Core.Actions.MyNotifications.UpdateWebPush
{
    public class MyNotificationsUpdateWebPushModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<MyNotificationsUpdateWebPushAction>();
        }
    }
}
