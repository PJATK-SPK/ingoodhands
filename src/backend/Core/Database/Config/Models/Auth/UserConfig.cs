using Core.Database.Models.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Database.Config.Models.Auth
{
    public class UserConfig<TBase> : IEntityTypeConfiguration<TBase>
    where TBase : User
    {
        public virtual void Configure(EntityTypeBuilder<TBase> builder)
        {
            builder.ToTable("users", "auth");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(c => c.Status).IsRequired();
            builder.Property(c => c.Status).IsRequired();
            builder.Property(c => c.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(c => c.LastName).HasMaxLength(50);
            builder.Property(c => c.PhoneNumber).HasMaxLength(20);
            builder.Property(c => c.Email).IsRequired().HasMaxLength(254);
            builder.HasIndex(c => c.Email).IsUnique().HasDatabaseName("users_email_idx");

            builder.HasOne(c => c.Warehouse).WithMany(c => (IEnumerable<TBase>?)c.Users).HasForeignKey(c => c.WarehouseId).HasConstraintName("warehouse_users_id_fkey");
        }
    }
}