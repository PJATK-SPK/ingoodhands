using Core.Database.Config.Base;
using Core.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Database.Config.Models
{
    public class UserConfig<TBase> : IEntityTypeConfiguration<TBase>
    where TBase : User
    {
        public virtual void Configure(EntityTypeBuilder<TBase> builder)
        {
            builder.ToTable("users", "core");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(c => c.Status).IsRequired();
            builder.Property(c => c.Status).IsRequired();
            builder.Property(c => c.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(c => c.LastName).HasMaxLength(50);
            builder.Property(c => c.Email).IsRequired().HasMaxLength(254);
            builder.HasIndex(c => c.Email).IsUnique().HasDatabaseName("users_email_idx");
        }
    }
}