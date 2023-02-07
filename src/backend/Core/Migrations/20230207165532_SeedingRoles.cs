using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Core.Migrations
{
    /// <inheritdoc />
    public partial class SeedingRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "auth",
                table: "roles",
                columns: new[] { "id", "name", "status", "update_user_id", "updated_at" },
                values: new object[,]
                {
                    { 1L, "Administrator", 0, 1L, new DateTime(2023, 2, 7, 16, 55, 32, 637, DateTimeKind.Utc).AddTicks(9644) },
                    { 2L, "Donor", 0, 1L, new DateTime(2023, 2, 7, 16, 55, 32, 637, DateTimeKind.Utc).AddTicks(9658) },
                    { 3L, "Needy", 0, 1L, new DateTime(2023, 2, 7, 16, 55, 32, 637, DateTimeKind.Utc).AddTicks(9666) },
                    { 4L, "WarehouseKeeper", 0, 1L, new DateTime(2023, 2, 7, 16, 55, 32, 637, DateTimeKind.Utc).AddTicks(9674) },
                    { 5L, "Deliverer", 0, 1L, new DateTime(2023, 2, 7, 16, 55, 32, 637, DateTimeKind.Utc).AddTicks(9681) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "auth",
                table: "roles",
                keyColumn: "id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                schema: "auth",
                table: "roles",
                keyColumn: "id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                schema: "auth",
                table: "roles",
                keyColumn: "id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                schema: "auth",
                table: "roles",
                keyColumn: "id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                schema: "auth",
                table: "roles",
                keyColumn: "id",
                keyValue: 5L);
        }
    }
}
