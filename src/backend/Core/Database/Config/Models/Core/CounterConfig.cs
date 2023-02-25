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
using Core.Database.Enums;

namespace Core.Database.Config.Models.Core
{
    public class CounterConfig<TBase> : IEntityTypeConfiguration<TBase>
    where TBase : Counter
    {
        public virtual void Configure(EntityTypeBuilder<TBase> builder)
        {
            new DbEntityConfig<TBase>().Configure(builder);
            builder.ToTable("counters", "core");

            builder.Property(c => c.Name).IsRequired().HasMaxLength(50).HasConversion(c => c.ToString(), c => Enum.Parse<TableName>(c!)); ;
            builder.Property(c => c.Value).IsRequired();

            builder.HasUniqueConstraint(c => c.Name);
        }
    }
}
