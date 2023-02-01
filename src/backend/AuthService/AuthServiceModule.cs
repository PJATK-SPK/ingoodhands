using AuthService.BusinessLogic.PostLogin;
using AuthService.BusinessLogic.GetAuth0UsersByCurrentUser;
using Autofac;
using Core.Autofac;
using AuthService.BusinessLogic.PatchUserDetails;
using AuthService.BusinessLogic.GetCurrentUser;
using AuthService.Services;

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
            builder.RegisterAsScoped<PatchUserDetailsPayloadDataValidationService>();
            builder.RegisterAsScoped<GetCurrentUserAction>();
            builder.RegisterAsScoped<GetCurrentUserService>();
        }
    }
}
