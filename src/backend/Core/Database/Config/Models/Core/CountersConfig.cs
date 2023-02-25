using Core.Database.Config.Base;
using Core.Database.Models.Core;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Database.Extensions;

namespace Core.Database.Config.Models.Core
{
    public class CountersConfig<TBase> : IEntityTypeConfiguration<TBase>
    where TBase : Counters
    {
        public virtual void Configure(EntityTypeBuilder<TBase> builder)
        {
            new DbEntityConfig<TBase>().Configure(builder);
            builder.ToTable("counters", "core");

            builder.Property(c => c.Name).IsRequired().HasMaxLength(50);
            builder.Property(c => c.Value).IsRequired().HasMaxLength(10);

            builder.HasUniqueConstraint(c => c.Name);
        }
    }
}
