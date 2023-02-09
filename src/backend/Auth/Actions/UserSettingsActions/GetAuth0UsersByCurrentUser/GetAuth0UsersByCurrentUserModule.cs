using Autofac;
using Core.Setup.Autofac;

namespace Auth.Actions.UserSettingsActions.GetAuth0UsersByCurrentUser
{
    public class GetAuth0UsersByCurrentUserModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<GetAuth0UsersByCurrentUserAction>();
            builder.RegisterAsScoped<GetAuth0UsersByCurrentUserService>();
        }
    }
}
