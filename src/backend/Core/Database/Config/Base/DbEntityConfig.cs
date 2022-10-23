using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Core.Database.Base;

namespace Core.Database.Config.Base
{
    internal class DbEntityConfig<TBase> : IEntityTypeConfiguration<TBase>
    where TBase : DbEntity
    {
        public void Configure(EntityTypeBuilder<TBase> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(c => c.UpdateUserId).IsRequired();
            builder.Property(c => c.UpdatedAt).IsRequired();
            builder.Property(c => c.Status).IsRequired();
            builder.HasOne(c => c.UpdateUser).WithMany().HasForeignKey(c => c.UpdateUserId);
        }
    }
}
