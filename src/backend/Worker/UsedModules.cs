using Autofac;
using Core;
using Donate;

namespace Worker
{
    public static class UsedModules
    {
        public static readonly IEnumerable<Module> List = new List<Module>()
        {
            new CoreModule(false),
            new DonateModule(),
        };
    }
}