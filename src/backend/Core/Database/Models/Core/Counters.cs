using Core.Database.Base;
using Core.Database.Config.Models.Core;
using Core.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Database.Models.Core
{
    public class Counters : DbEntity, IEntityTypeConfiguration<Counters>
    {
        public string? Name { get; set; }
        public long Value { get; set; }
        public void Configure(EntityTypeBuilder<Counters> builder)
         => new CountersConfig<Counters>().Configure(builder);

        public override bool Equals(object? obj) => ReferenceEquals(obj, this);

        public override int GetHashCode()
            => HashCode.Combine(base.GetHashCode(), Name, Value);
    }
}
