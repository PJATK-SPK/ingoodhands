using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Core.Migrations
{
    /// <inheritdoc />
    public partial class AddAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "addresses",
                schema: "core",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    countryid = table.Column<long>(name: "country_id", type: "bigint", nullable: false),
                    postalcode = table.Column<string>(name: "postal_code", type: "character varying(10)", maxLength: 10, nullable: false),
                    city = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    street = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    streetnumber = table.Column<string>(name: "street_number", type: "character varying(10)", maxLength: 10, nullable: true),
                    apartment = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    gpslatitude = table.Column<double>(name: "gps_latitude", type: "double precision", nullable: false),
                    gpslongitude = table.Column<double>(name: "gps_longitude", type: "double precision", nullable: false),
                    updateuserid = table.Column<long>(name: "update_user_id", type: "bigint", nullable: false),
                    updatedat = table.Column<DateTime>(name: "updated_at", type: "timestamp with time zone", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_addresses", x => x.id);
                    table.ForeignKey(
                        name: "addresses_country_id_fkey",
                        column: x => x.countryid,
                        principalSchema: "core",
                        principalTable: "countries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_addresses_users_update_user_id",
                        column: x => x.updateuserid,
                        principalSchema: "core",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_addresses_country_id",
                schema: "core",
                table: "addresses",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "ix_addresses_update_user_id",
                schema: "core",
                table: "addresses",
                column: "update_user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "addresses",
                schema: "core");
        }
    }
}
