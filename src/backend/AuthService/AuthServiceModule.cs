using Autofac;
using Core.Autofac;
using AuthService.Services;
using AuthService.Actions.AuthActions.PostLogin;
using AuthService.Actions.UserSettingsActions.PatchUserDetails;
using AuthService.Actions.UserSettingsActions.GetAuth0UsersByCurrentUser;
using AuthService.Actions.UserSettingsActions.GetUserDetails;

namespace AuthService
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
