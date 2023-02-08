﻿// <auto-generated />
using System;
using Core.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Core.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230208184157_AddWarehouse")]
    partial class AddWarehouse
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Core.Database.Models.Auth.Auth0User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(254)
                        .HasColumnType("character varying(254)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("first_name");

                    b.Property<string>("Identifier")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("character varying(80)")
                        .HasColumnName("identifier");

                    b.Property<string>("LastName")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("last_name");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("nickname");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<long?>("UpdateUserId")
                        .IsRequired()
                        .HasColumnType("bigint")
                        .HasColumnName("update_user_id");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_auth0_users");

                    b.HasIndex("Identifier")
                        .IsUnique()
                        .HasDatabaseName("ix_auth0_users_identifier");

                    b.HasIndex("UpdateUserId")
                        .HasDatabaseName("ix_auth0_users_update_user_id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_auth0_users_user_id");

                    b.HasIndex("Status", "Identifier")
                        .HasDatabaseName("auth_users_status_identifier_idx");

                    b.ToTable("auth0_users", "auth");
                });

            modelBuilder.Entity("Core.Database.Models.Auth.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)")
                        .HasColumnName("name");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<long?>("UpdateUserId")
                        .IsRequired()
                        .HasColumnType("bigint")
                        .HasColumnName("update_user_id");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("pk_roles");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("ix_roles_name");

                    b.HasIndex("UpdateUserId")
                        .HasDatabaseName("ix_roles_update_user_id");

                    b.ToTable("roles", "auth");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Name = "Administrator",
                            Status = 0,
                            UpdateUserId = 1L,
                            UpdatedAt = new DateTime(2023, 2, 8, 18, 41, 57, 344, DateTimeKind.Utc).AddTicks(2570)
                        },
                        new
                        {
                            Id = 2L,
                            Name = "Donor",
                            Status = 0,
                            UpdateUserId = 1L,
                            UpdatedAt = new DateTime(2023, 2, 8, 18, 41, 57, 344, DateTimeKind.Utc).AddTicks(2590)
                        },
                        new
                        {
                            Id = 3L,
                            Name = "Needy",
                            Status = 0,
                            UpdateUserId = 1L,
                            UpdatedAt = new DateTime(2023, 2, 8, 18, 41, 57, 344, DateTimeKind.Utc).AddTicks(2600)
                        },
                        new
                        {
                            Id = 4L,
                            Name = "WarehouseKeeper",
                            Status = 0,
                            UpdateUserId = 1L,
                            UpdatedAt = new DateTime(2023, 2, 8, 18, 41, 57, 344, DateTimeKind.Utc).AddTicks(2610)
                        },
                        new
                        {
                            Id = 5L,
                            Name = "Deliverer",
                            Status = 0,
                            UpdateUserId = 1L,
                            UpdatedAt = new DateTime(2023, 2, 8, 18, 41, 57, 344, DateTimeKind.Utc).AddTicks(2620)
                        });
                });

            modelBuilder.Entity("Core.Database.Models.Auth.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(254)
                        .HasColumnType("character varying(254)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("last_name");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasDatabaseName("users_email_idx");

                    b.ToTable("users", "auth");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Email = "service@service.com",
                            FirstName = "Service",
                            LastName = "Service",
                            Status = 0
                        });
                });

            modelBuilder.Entity("Core.Database.Models.Auth.UserRole", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint")
                        .HasColumnName("role_id");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<long?>("UpdateUserId")
                        .IsRequired()
                        .HasColumnType("bigint")
                        .HasColumnName("update_user_id");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_user_roles");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_user_roles_role_id");

                    b.HasIndex("UpdateUserId")
                        .HasDatabaseName("ix_user_roles_update_user_id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_user_roles_user_id");

                    b.ToTable("user_roles", "auth");
                });

            modelBuilder.Entity("Core.Database.Models.Core.Address", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Apartment")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("apartment");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("city");

                    b.Property<long>("CountryId")
                        .HasColumnType("bigint")
                        .HasColumnName("country_id");

                    b.Property<double>("GpsLatitude")
                        .HasColumnType("double precision")
                        .HasColumnName("gps_latitude");

                    b.Property<double>("GpsLongitude")
                        .HasColumnType("double precision")
                        .HasColumnName("gps_longitude");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("postal_code");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<string>("Street")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("street");

                    b.Property<string>("StreetNumber")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("street_number");

                    b.Property<long?>("UpdateUserId")
                        .IsRequired()
                        .HasColumnType("bigint")
                        .HasColumnName("update_user_id");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("pk_addresses");

                    b.HasIndex("CountryId")
                        .HasDatabaseName("ix_addresses_country_id");

                    b.HasIndex("UpdateUserId")
                        .HasDatabaseName("ix_addresses_update_user_id");

                    b.ToTable("addresses", "core");
                });

            modelBuilder.Entity("Core.Database.Models.Core.Country", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Alpha2IsoCode")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("character varying(2)")
                        .HasColumnName("alpha2iso_code");

                    b.Property<string>("Alpha3IsoCode")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("character varying(3)")
                        .HasColumnName("alpha3iso_code");

                    b.Property<string>("EnglishName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("english_name");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<long?>("UpdateUserId")
                        .IsRequired()
                        .HasColumnType("bigint")
                        .HasColumnName("update_user_id");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("pk_countries");

                    b.HasIndex("Alpha2IsoCode")
                        .IsUnique()
                        .HasDatabaseName("ix_countries_alpha2iso_code");

                    b.HasIndex("Alpha3IsoCode")
                        .IsUnique()
                        .HasDatabaseName("ix_countries_alpha3iso_code");

                    b.HasIndex("UpdateUserId")
                        .HasDatabaseName("ix_countries_update_user_id");

                    b.ToTable("countries", "core");
                });

            modelBuilder.Entity("Core.Database.Models.Core.Warehouse", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("AddressId")
                        .HasColumnType("bigint")
                        .HasColumnName("address_id");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("character varying(5)")
                        .HasColumnName("short_name");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<long?>("UpdateUserId")
                        .IsRequired()
                        .HasColumnType("bigint")
                        .HasColumnName("update_user_id");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("pk_warehouses");

                    b.HasIndex("AddressId")
                        .HasDatabaseName("ix_warehouses_address_id");

                    b.HasIndex("UpdateUserId")
                        .HasDatabaseName("ix_warehouses_update_user_id");

                    b.ToTable("warehouses", "core");
                });

            modelBuilder.Entity("Core.Database.Models.Auth.Auth0User", b =>
                {
                    b.HasOne("Core.Database.Models.Auth.User", "UpdateUser")
                        .WithMany()
                        .HasForeignKey("UpdateUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_auth0_users_users_update_user_id");

                    b.HasOne("Core.Database.Models.Auth.User", "User")
                        .WithMany("Auth0Users")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("auth_users_user_id_fkey");

                    b.Navigation("UpdateUser");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Core.Database.Models.Auth.Role", b =>
                {
                    b.HasOne("Core.Database.Models.Auth.User", "UpdateUser")
                        .WithMany()
                        .HasForeignKey("UpdateUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_roles_users_update_user_id");

                    b.Navigation("UpdateUser");
                });

            modelBuilder.Entity("Core.Database.Models.Auth.UserRole", b =>
                {
                    b.HasOne("Core.Database.Models.Auth.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("user_roles_role_id_fkey");

                    b.HasOne("Core.Database.Models.Auth.User", "UpdateUser")
                        .WithMany()
                        .HasForeignKey("UpdateUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_roles_users_update_user_id");

                    b.HasOne("Core.Database.Models.Auth.User", "User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("user_roles_user_id_fkey");

                    b.Navigation("Role");

                    b.Navigation("UpdateUser");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Core.Database.Models.Core.Address", b =>
                {
                    b.HasOne("Core.Database.Models.Core.Country", "Country")
                        .WithMany("Addresses")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("addresses_country_id_fkey");

                    b.HasOne("Core.Database.Models.Auth.User", "UpdateUser")
                        .WithMany()
                        .HasForeignKey("UpdateUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_addresses_users_update_user_id");

                    b.Navigation("Country");

                    b.Navigation("UpdateUser");
                });

            modelBuilder.Entity("Core.Database.Models.Core.Country", b =>
                {
                    b.HasOne("Core.Database.Models.Auth.User", "UpdateUser")
                        .WithMany()
                        .HasForeignKey("UpdateUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_countries_users_update_user_id");

                    b.Navigation("UpdateUser");
                });

            modelBuilder.Entity("Core.Database.Models.Core.Warehouse", b =>
                {
                    b.HasOne("Core.Database.Models.Core.Address", "Address")
                        .WithMany("Warehouses")
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("warehouses_address_id_fkey");

                    b.HasOne("Core.Database.Models.Auth.User", "UpdateUser")
                        .WithMany()
                        .HasForeignKey("UpdateUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_warehouses_users_update_user_id");

                    b.Navigation("Address");

                    b.Navigation("UpdateUser");
                });

            modelBuilder.Entity("Core.Database.Models.Auth.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Core.Database.Models.Auth.User", b =>
                {
                    b.Navigation("Auth0Users");

                    b.Navigation("Roles");
                });

            modelBuilder.Entity("Core.Database.Models.Core.Address", b =>
                {
                    b.Navigation("Warehouses");
                });

            modelBuilder.Entity("Core.Database.Models.Core.Country", b =>
                {
                    b.Navigation("Addresses");
                });
#pragma warning restore 612, 618
        }
    }
}
