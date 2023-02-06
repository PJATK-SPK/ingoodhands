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
        public DbSet<Country> Countries { get; set; } = default!;
        public DbSet<Address> Addresses { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = AppDomain.CurrentDomain.GetAssemblies()
                .Where(c => c.GetName().Name!.Contains("Core"))
                .Single(c => c.CustomAttributes
                    .SelectMany(s => s.ConstructorArguments
                        .Where(z => z.Value is string)
                        .Select(z => (string)z.Value!)
                    ).Contains("PJATK"));

            modelBuilder.ApplyConfigurationsFromAssembly(assembly);

            UserSeeder.Execute(modelBuilder);
        }
    }
}
