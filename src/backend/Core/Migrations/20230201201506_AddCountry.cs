using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Core.Migrations
{
    /// <inheritdoc />
    public partial class AddCountry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "countries",
                schema: "core",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    englishname = table.Column<string>(name: "english_name", type: "character varying(50)", maxLength: 50, nullable: false),
                    alpha2isocode = table.Column<string>(name: "alpha2iso_code", type: "character varying(2)", maxLength: 2, nullable: false),
                    alpha3isocode = table.Column<string>(name: "alpha3iso_code", type: "character varying(3)", maxLength: 3, nullable: false),
                    updateuserid = table.Column<long>(name: "update_user_id", type: "bigint", nullable: false),
                    updatedat = table.Column<DateTime>(name: "updated_at", type: "timestamp with time zone", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_countries", x => x.id);
                    table.ForeignKey(
                        name: "fk_countries_users_update_user_id",
                        column: x => x.updateuserid,
                        principalSchema: "core",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_countries_alpha2iso_code",
                schema: "core",
                table: "countries",
                column: "alpha2iso_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_countries_alpha3iso_code",
                schema: "core",
                table: "countries",
                column: "alpha3iso_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_countries_update_user_id",
                schema: "core",
                table: "countries",
                column: "update_user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "countries",
                schema: "core");
        }
    }
}
