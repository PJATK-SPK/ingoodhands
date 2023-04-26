using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Core.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreProductsToSeeder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "core",
                table: "products",
                columns: new[] { "id", "name", "status", "unit", "update_user_id", "updated_at" },
                values: new object[,]
                {
                    { 20L, "Helmet", 0, "Pcs", 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 21L, "Bulletproof vest", 0, "Pcs", 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 22L, "Food ration", 0, "Pcs", 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 23L, "Military boots", 0, "Pcs", 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 24L, "Winter jacket", 0, "Pcs", 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 25L, "Winter pants", 0, "Pcs", 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 26L, "Military uniform", 0, "Pcs", 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 27L, "Cell phone", 0, "Pcs", 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "core",
                table: "products",
                keyColumn: "id",
                keyValue: 20L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "products",
                keyColumn: "id",
                keyValue: 21L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "products",
                keyColumn: "id",
                keyValue: 22L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "products",
                keyColumn: "id",
                keyValue: 23L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "products",
                keyColumn: "id",
                keyValue: 24L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "products",
                keyColumn: "id",
                keyValue: 25L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "products",
                keyColumn: "id",
                keyValue: 26L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "products",
                keyColumn: "id",
                keyValue: 27L);
        }
    }
}
