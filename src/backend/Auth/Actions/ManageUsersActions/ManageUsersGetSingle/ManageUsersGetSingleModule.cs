using Autofac;
using Core.Setup.Autofac;

namespace Auth.Actions.ManageUsersActions.ManageUsersGetSingle
{
    public class ManageUsersGetSingleModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<ManageUsersGetSingleAction>();
        }
    }
}
