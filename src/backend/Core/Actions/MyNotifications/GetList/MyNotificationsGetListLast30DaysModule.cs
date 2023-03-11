using Autofac;
using Core.Setup.Autofac;

namespace Core.Actions.MyNotifications.GetList
{
    public class MyNotificationsGetListLast30DaysModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<MyNotificationsGetListLast30DaysAction>();
        }
    }
}
