using Auth;
using Autofac;
using Core;
using Core.Setup.Enums;
using Donate;

namespace WebApi
{
    public static class UsedModules
    {
        public static readonly IEnumerable<Module> List = new List<Module>()
        {
            new CoreModule(WebApiUserProviderType.ProvideByLoggedAuth0User),
            new AuthModule(),
            new DonateModule(),
        };
    }
}