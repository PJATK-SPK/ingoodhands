using Autofac;
using Core.Autofac;
using Auth.Services;
using Auth.Actions.AuthActions.PostLogin;
using Auth.Actions.UserSettingsActions.PatchUserDetails;
using Auth.Actions.UserSettingsActions.GetAuth0UsersByCurrentUser;
using Auth.Actions.UserSettingsActions.GetUserDetails;

namespace Auth
{
    public class AuthServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<PostLoginAction>();
            builder.RegisterAsScoped<UserDataValidationService>();
            builder.RegisterAsScoped<UserCreationService>();
            builder.RegisterAsScoped<GetAuth0UsersByCurrentUserAction>();
            builder.RegisterAsScoped<GetAuth0UsersByCurrentUserService>();
            builder.RegisterAsScoped<PatchUserDetailsAction>();
            builder.RegisterAsScoped<PatchUserDetailsPayload>();
            builder.RegisterAsScoped<PatchUserDetailsService>();
            builder.RegisterAsScoped<PatchUserDetailsPayloadValidator>();
            builder.RegisterAsScoped<GetUserDetailsAction>();
            builder.RegisterAsScoped<GetUserDetailsService>();
        }
    }
}
