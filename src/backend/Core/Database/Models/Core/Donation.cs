﻿using Core.Database.Base;
using Core.Database.Config.Models.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Database.Models.Core
{
    public class Donation : DbEntity, IEntityTypeConfiguration<Donation>
    {
        public long CreationUserId { get; set; }
        public DateTime CreationDate { get; set; }
        public long WarehouseId { get; set; }
        public Warehouse? Warehouse { get; set; }
        public string Name { get; set; } = default!;
        public bool IsExpired { get; set; }
        public bool IsDelivered { get; set; }
        public bool IsIncludedInStock { get; set; }

        public List<Product>? Products { get; set; }

        public void Configure(EntityTypeBuilder<Donation> builder)
           => new DonationConfig<Donation>().Configure(builder);

        public override bool Equals(object? obj) => ReferenceEquals(obj, this);

        public override int GetHashCode()
            => HashCode.Combine(base.GetHashCode(), CreationUserId, CreationDate, WarehouseId, Name, IsExpired, IsDelivered, IsIncludedInStock);
    }
}
