using Auth.Services;
using Autofac;
using Core.Setup.Autofac;

namespace Auth.Actions.UserSettingsActions.GetUserDetails
{
    public class GetUserDetailsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<GetUserDetailsAction>();
        }
    }
}
