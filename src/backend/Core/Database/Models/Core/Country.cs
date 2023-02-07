﻿using Core.Database.Base;
using Core.Database.Config.Models.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Database.Models.Core
{
    public class Country : DbEntity, IEntityTypeConfiguration<Country>
    {
        public string EnglishName { get; set; } = default!;
        public string Alpha2IsoCode { get; set; } = default!;
        public string Alpha3IsoCode { get; set; } = default!;

        public List<Address>? Addresses { get; set; }

        public void Configure(EntityTypeBuilder<Country> builder)
            => new CountryConfig<Country>().Configure(builder);

        public override bool Equals(object? obj) => ReferenceEquals(obj, this);

        public override int GetHashCode()
            => HashCode.Combine(base.GetHashCode(), EnglishName, Alpha2IsoCode, Alpha3IsoCode);
    }
}