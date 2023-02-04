using AuthService;
using Autofac;
using Core;

namespace WebApi
{
    public static class UsedModules
    {
        public static readonly IEnumerable<Module> List = new List<Module>()
        {
            new CoreModule(true),
            new AuthServiceModule(),
        };
    }
}