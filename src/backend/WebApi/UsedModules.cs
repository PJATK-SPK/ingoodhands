using Auth;
using Autofac;
using Core;
using Donate;

namespace WebApi
{
    public static class UsedModules
    {
        public static readonly IEnumerable<Module> List = new List<Module>()
        {
            new CoreModule(true),
            new AuthModule(),
            new DonateModule(),
        };
    }
}