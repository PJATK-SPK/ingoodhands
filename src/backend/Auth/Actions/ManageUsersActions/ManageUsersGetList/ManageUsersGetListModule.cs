using Autofac;
using Core.Setup.Autofac;

namespace Auth.Actions.ManageUsersActions.ManageUsersGetList
{
    public class ManageUsersGetListModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<ManageUsersGetListAction>();
        }
    }
}
