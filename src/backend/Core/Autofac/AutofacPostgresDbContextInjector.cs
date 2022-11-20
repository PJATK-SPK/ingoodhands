using Autofac;
using Core.Database;
using Microsoft.EntityFrameworkCore;

namespace Core.Autofac
{
    public static class AutofacPostgresDbContextInjector
    {
        public static void Inject(ContainerBuilder builder, string connectionString)
        {
            builder.Register(x =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
                optionsBuilder.UseNpgsql(connectionString);
                optionsBuilder.UseSnakeCaseNamingConvention();
                var result = new AppDbContext(optionsBuilder.Options);
                result.Database.Migrate();
                return result;
            }).InstancePerLifetimeScope();
        }
    }
}
