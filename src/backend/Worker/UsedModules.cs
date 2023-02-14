using Autofac;
using Core;
using Core.Setup.Enums;
using Donate;

namespace Worker
{
    public static class UsedModules
    {
        public static readonly IEnumerable<Module> List = new List<Module>()
        {
            new CoreModule(WebApiUserProviderType.ProvideServiceUser),
            new DonateModule(),
        };
    }
}