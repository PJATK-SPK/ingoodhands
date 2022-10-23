using Core.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; } = default!;
        public DbSet<AuthUser> AuthUsers { get; set; } = default!;
        public DbSet<Permission> Permissions { get; set; } = default!;

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
        }
    }
}
