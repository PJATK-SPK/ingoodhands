using AuthService.BusinessLogic.PostLogin;
using Autofac;
using Core;
using Core.Autofac;
using Core.Database;

namespace AuthService
{
    public class AuthServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAsScoped<PostLoginAction>();
            builder.RegisterAsScoped<UserDataValidationService>();
            builder.RegisterAsScoped<UserCreationService>();
        }
    }
}
