using Autofac;
using Core.Auth0;
using Core.Autofac;
using Core.ConfigSetup;
using Core.Database;
using HashidsNet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using TestsBase;

namespace TestsCore
{
#pragma warning disable S3881
    public class TestsToolkit : IDisposable
    {

        private readonly ILifetimeScope _autofac;
        public readonly TestType TestType;

        public TestsToolkit(IEnumerable<Module> modules, TestType type = TestType.Integration)
        {
            TestType = type;

            var builder = new ContainerBuilder();

            foreach (var module in modules)
                builder.RegisterModule(module);

            InjectCurrentUserService(builder);
            InjectTestDatabase(builder);
            FixILogger(builder);

            var container = builder.Build();
            _autofac = container.BeginLifetimeScope();

            if (type == TestType.Integration)
                _autofac.Resolve<AppDbContext>().Database.Migrate();
        }

        public void UpdateUserInfo(CurrentUserInfo info)
        {
            var service = _autofac.Resolve<TestsCurrentUserService>();
            service.Update(info);
        }

        public T Resolve<T>() where T : notnull => _autofac.Resolve<T>();

        public Hashids Hashids => _autofac.Resolve<Hashids>();

        public void Dispose()
        {
            if (TestType == TestType.Unit)
            {
                return;
            }

            var appDbContext = _autofac.Resolve<AppDbContext>();
            var databaseName = appDbContext.Database.GetDbConnection().Database;

            var connectionString = ConfigurationReader.Get().ConnectionStrings.Database;
            connectionString = connectionString.Replace("Database=in_good_hands", "Database=postgres");

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseNpgsql(connectionString);
            var tempDbContext = new AppDbContext(optionsBuilder.Options);

#pragma warning disable S2077
            tempDbContext.Database.ExecuteSqlRaw($"DROP DATABASE {databaseName} WITH (FORCE);");
#pragma warning restore S2077
        }

        private static void InjectCurrentUserService(ContainerBuilder builder)
        {
            builder.RegisterType<TestsCurrentUserService>().AsSelf().As<ICurrentUserService>().SingleInstance();
        }

        private static void FixILogger(ContainerBuilder builder)
        {
#pragma warning disable S4792
            builder.RegisterInstance(new LoggerFactory()).As<ILoggerFactory>();
#pragma warning restore S4792
            builder.RegisterGeneric(typeof(Logger<>)).As(typeof(ILogger<>)).SingleInstance();
        }

        private void InjectTestDatabase(ContainerBuilder builder)
        {
            var connectionString = ConfigurationReader.Get().ConnectionStrings.Database;
            var yyyyMMddHHmmss = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            var randomNumber = RandomNumberGenerator.GetInt32(1000, 9999);

            if (TestType == TestType.Unit)
                connectionString = connectionString.Replace("Database=FAKEDB", $"Database=THIS_SHOULD_NOT_HAPPEN");
            else if (TestType == TestType.Integration)
                connectionString = connectionString.Replace("Database=in_good_hands", $"Database=tests_{yyyyMMddHHmmss}_{randomNumber}");

            AutofacPostgresDbContextInjector.Inject(builder, connectionString);
        }
    }
#pragma warning disable S3881
}
