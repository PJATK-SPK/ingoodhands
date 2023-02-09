using Autofac;
using Auth.Services;
using Auth.Actions.AuthActions.PostLogin;
using Auth.Actions.UserSettingsActions.PatchUserDetails;
using Auth.Actions.UserSettingsActions.GetAuth0UsersByCurrentUser;
using Core.Setup.Autofac;

namespace Auth.Actions.AuthActions.PostLogin
{
    public class PostLoginModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<PostLoginAction>();
            builder.RegisterAsScoped<PostLoginUserCreationService>();
        }
    }
}
