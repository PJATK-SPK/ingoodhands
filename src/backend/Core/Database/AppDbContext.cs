using Core.Database.Models.Auth;
using Core.Database.Models.Core;
using Core.Database.Seeders;
using Microsoft.EntityFrameworkCore;

namespace Core.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; } = default!;
        public DbSet<Auth0User> Auth0Users { get; set; } = default!;
        public DbSet<UserRole> UserRoles { get; set; } = default!;
        public DbSet<Role> Roles { get; set; } = default!;
        public DbSet<Counters> Counters { get; set; } = default!;
        public DbSet<Country> Countries { get; set; } = default!;
        public DbSet<Address> Addresses { get; set; } = default!;
        public DbSet<Warehouse> Warehouses { get; set; } = default!;
        public DbSet<Product> Products { get; set; } = default!;
        public DbSet<Donation> Donations { get; set; } = default!;
        public DbSet<DonationProduct> DonationProducts { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ApplyConfigurations(modelBuilder);
            SeedInitalData(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
#if DEBUG
            optionsBuilder.EnableSensitiveDataLogging();
#endif
        }

        private static void ApplyConfigurations(ModelBuilder modelBuilder)
        {
            var assembly = AppDomain.CurrentDomain.GetAssemblies()
              .Where(c => c.GetName().Name!.Contains("Core"))
              .Single(c => c.CustomAttributes
                  .SelectMany(s => s.ConstructorArguments
                      .Where(z => z.Value is string)
                      .Select(z => (string)z.Value!)
              ).Contains("PJATK"));

            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        }

        private static void SeedInitalData(ModelBuilder modelBuilder)
        {
            UserSeeder.Execute(modelBuilder);
            RoleSeeder.Execute(modelBuilder);
            CountrySeeder.Execute(modelBuilder);
            AddressSeeder.Execute(modelBuilder);
            WarehouseSeeder.Execute(modelBuilder);
            ProductSeeder.Execute(modelBuilder);
            CountersSeeder.Execute(modelBuilder);
        }
    }
}
