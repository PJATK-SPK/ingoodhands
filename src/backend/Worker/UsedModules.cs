using Autofac;
using Core;

public static class UsedModules
{
    public static readonly IEnumerable<Module> List = new List<Module>()
    {
        new CoreModule(false),
    };
}