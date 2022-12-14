using Autofac;
using Core.Auth0;
using Core.Autofac;
using Core.ConfigSetup;
using Core.Database;
using Microsoft.EntityFrameworkCore;

namespace TestsCore
{
    public class TestsToolkit : IDisposable
    {
        private readonly ILifetimeScope _autofac;

        public TestsToolkit(IEnumerable<Module> modules)
        {
            var builder = new ContainerBuilder();

            foreach (var module in modules)
                builder.RegisterModule(module);

            InjectCurrentUserService(builder);
            ChangeConnectionString(builder);

            var container = builder.Build();
            _autofac = container.BeginLifetimeScope();
            _autofac.Resolve<AppDbContext>().Database.Migrate();
        }

        public void UpdateUserInfo(CurrentUserInfo info)
        {
            var service = _autofac.Resolve<TestsCurrentUserService>();
            service.Update(info);
        }

        public T Resolve<T>() where T : notnull => _autofac.Resolve<T>();

        public void Dispose()
        {
            var appDbContext = _autofac.Resolve<AppDbContext>();
            var databaseName = appDbContext.Database.GetDbConnection().Database;

            var connectionString = ConfigurationReader.Get().ConnectionStrings.Database;
            connectionString = connectionString.Replace("Database=in_good_hands", "Database=postgres");

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseNpgsql(connectionString);
            var tempDbContext = new AppDbContext(optionsBuilder.Options);

            tempDbContext.Database.ExecuteSqlRaw($"DROP DATABASE {databaseName} WITH (FORCE);");
        }

        private void InjectCurrentUserService(ContainerBuilder builder)
        {
            builder.RegisterType<TestsCurrentUserService>().AsSelf().As<ICurrentUserService>().SingleInstance();
        }

        private void ChangeConnectionString(ContainerBuilder builder)
        {
            var connectionString = ConfigurationReader.Get().ConnectionStrings.Database;
            var yyyyMMddHHmmss = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            var randomNumber = new Random().Next(1000, 9999);
            connectionString = connectionString.Replace("Database=in_good_hands", $"Database=tests_{yyyyMMddHHmmss}_{randomNumber}");
            AutofacPostgresDbContextInjector.Inject(builder, connectionString);
        }
    }
}
