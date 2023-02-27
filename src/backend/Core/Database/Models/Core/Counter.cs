using Core.Database.Base;
using Core.Database.Config.Models.Core;
using Core.Database.Enums;
using Core.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Database.Models.Core
{
    public class Counter : DbEntity, IEntityTypeConfiguration<Counter>
    {
        public TableName Name { get; set; }
        public long Value { get; set; }
        public void Configure(EntityTypeBuilder<Counter> builder)
         => new CounterConfig<Counter>().Configure(builder);

        public override bool Equals(object? obj) => ReferenceEquals(obj, this);

        public override int GetHashCode()
            => HashCode.Combine(base.GetHashCode(), Name, Value);
    }
}
