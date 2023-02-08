using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Core.Migrations
{
    /// <inheritdoc />
    public partial class AddWarehouse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "warehouses",
                schema: "core",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    addressid = table.Column<long>(name: "address_id", type: "bigint", nullable: false),
                    shortname = table.Column<string>(name: "short_name", type: "character varying(5)", maxLength: 5, nullable: false),
                    updateuserid = table.Column<long>(name: "update_user_id", type: "bigint", nullable: false),
                    updatedat = table.Column<DateTime>(name: "updated_at", type: "timestamp with time zone", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_warehouses", x => x.id);
                    table.ForeignKey(
                        name: "fk_warehouses_users_update_user_id",
                        column: x => x.updateuserid,
                        principalSchema: "auth",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "warehouses_address_id_fkey",
                        column: x => x.addressid,
                        principalSchema: "core",
                        principalTable: "addresses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                schema: "auth",
                table: "roles",
                keyColumn: "id",
                keyValue: 1L,
                column: "updated_at",
                value: new DateTime(2023, 2, 8, 18, 41, 57, 344, DateTimeKind.Utc).AddTicks(2570));

            migrationBuilder.UpdateData(
                schema: "auth",
                table: "roles",
                keyColumn: "id",
                keyValue: 2L,
                column: "updated_at",
                value: new DateTime(2023, 2, 8, 18, 41, 57, 344, DateTimeKind.Utc).AddTicks(2590));

            migrationBuilder.UpdateData(
                schema: "auth",
                table: "roles",
                keyColumn: "id",
                keyValue: 3L,
                column: "updated_at",
                value: new DateTime(2023, 2, 8, 18, 41, 57, 344, DateTimeKind.Utc).AddTicks(2600));

            migrationBuilder.UpdateData(
                schema: "auth",
                table: "roles",
                keyColumn: "id",
                keyValue: 4L,
                column: "updated_at",
                value: new DateTime(2023, 2, 8, 18, 41, 57, 344, DateTimeKind.Utc).AddTicks(2610));

            migrationBuilder.UpdateData(
                schema: "auth",
                table: "roles",
                keyColumn: "id",
                keyValue: 5L,
                column: "updated_at",
                value: new DateTime(2023, 2, 8, 18, 41, 57, 344, DateTimeKind.Utc).AddTicks(2620));

            migrationBuilder.CreateIndex(
                name: "ix_warehouses_address_id",
                schema: "core",
                table: "warehouses",
                column: "address_id");

            migrationBuilder.CreateIndex(
                name: "ix_warehouses_update_user_id",
                schema: "core",
                table: "warehouses",
                column: "update_user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "warehouses",
                schema: "core");

            migrationBuilder.UpdateData(
                schema: "auth",
                table: "roles",
                keyColumn: "id",
                keyValue: 1L,
                column: "updated_at",
                value: new DateTime(2023, 2, 7, 16, 55, 32, 637, DateTimeKind.Utc).AddTicks(9644));

            migrationBuilder.UpdateData(
                schema: "auth",
                table: "roles",
                keyColumn: "id",
                keyValue: 2L,
                column: "updated_at",
                value: new DateTime(2023, 2, 7, 16, 55, 32, 637, DateTimeKind.Utc).AddTicks(9658));

            migrationBuilder.UpdateData(
                schema: "auth",
                table: "roles",
                keyColumn: "id",
                keyValue: 3L,
                column: "updated_at",
                value: new DateTime(2023, 2, 7, 16, 55, 32, 637, DateTimeKind.Utc).AddTicks(9666));

            migrationBuilder.UpdateData(
                schema: "auth",
                table: "roles",
                keyColumn: "id",
                keyValue: 4L,
                column: "updated_at",
                value: new DateTime(2023, 2, 7, 16, 55, 32, 637, DateTimeKind.Utc).AddTicks(9674));

            migrationBuilder.UpdateData(
                schema: "auth",
                table: "roles",
                keyColumn: "id",
                keyValue: 5L,
                column: "updated_at",
                value: new DateTime(2023, 2, 7, 16, 55, 32, 637, DateTimeKind.Utc).AddTicks(9681));
        }
    }
}
