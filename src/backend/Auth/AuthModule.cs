using Autofac;
using Auth.Services;
using Auth.Actions.AuthActions.PostLogin;
using Auth.Actions.UserSettingsActions.PatchUserDetails;
using Auth.Actions.UserSettingsActions.GetAuth0UsersByCurrentUser;
using Auth.Actions.UserSettingsActions.GetUserDetails;
using Core.Setup.Autofac;

namespace Auth
{
    public class AuthModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            RegisterActions(builder);
            RegisterServices(builder);
        }

        private static void RegisterActions(ContainerBuilder builder)
        {
            builder.RegisterModule<PostLoginModule>();
            builder.RegisterModule<GetAuth0UsersByCurrentUserModule>();
            builder.RegisterModule<GetUserDetailsModule>();
            builder.RegisterModule<PatchUserDetailsModule>();
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<CurrentUserInfoValidator>();
        }
    }
}
