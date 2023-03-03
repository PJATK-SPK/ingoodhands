using Autofac;
using Core.Setup.Autofac;

namespace Auth.Actions.ManageUsersActions.GetList
{
    public class GetListModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<GetListAction>();
        }
    }
}
