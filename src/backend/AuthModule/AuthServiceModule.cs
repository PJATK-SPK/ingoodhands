using Autofac;

namespace AuthService
{
    public class AuthServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterAsScoped<MyServiceClass>(); ...
        }
    }
}
