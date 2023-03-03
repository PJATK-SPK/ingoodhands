using Autofac;
using Core.Setup.Autofac;

namespace Auth.Actions.ManageUsersActions.GetSingle
{
    public class GetSingleModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<GetSingleAction>();
        }
    }
}
