using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Core.Migrations
{
    /// <inheritdoc />
    public partial class AddProductData : Migration
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
                    { 1L, "Rice", 0, "Kg", 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2L, "Pasta", 0, "Kg", 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 3L, "Cereals", 0, "Kg", 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 4L, "Groats", 0, "Kg", 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 5L, "Walnuts", 0, "Kg", 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 6L, "Delicacies", 0, "Kg", 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 7L, "Flour", 0, "Kg", 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 8L, "Water", 0, "L", 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 9L, "Milk", 0, "L", 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 10L, "Bubble bath", 0, "L", 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 11L, "Juice", 0, "L", 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 12L, "Energy drink", 0, "L", 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 13L, "Soup", 0, "L", 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 14L, "Canned food", 0, "Pcs", 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 15L, "Toothbrushes", 0, "Pcs", 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 16L, "Diapers", 0, "Pcs", 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 17L, "Disinfectants", 0, "Pcs", 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 18L, "Toilet paper", 0, "Pcs", 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 19L, "Medicines", 0, "Pcs", 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "core",
                table: "products",
                keyColumn: "id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "products",
                keyColumn: "id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "products",
                keyColumn: "id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "products",
                keyColumn: "id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "products",
                keyColumn: "id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "products",
                keyColumn: "id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "products",
                keyColumn: "id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "products",
                keyColumn: "id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "products",
                keyColumn: "id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "products",
                keyColumn: "id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "products",
                keyColumn: "id",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "products",
                keyColumn: "id",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "products",
                keyColumn: "id",
                keyValue: 13L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "products",
                keyColumn: "id",
                keyValue: 14L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "products",
                keyColumn: "id",
                keyValue: 15L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "products",
                keyColumn: "id",
                keyValue: 16L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "products",
                keyColumn: "id",
                keyValue: 17L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "products",
                keyColumn: "id",
                keyValue: 18L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "products",
                keyColumn: "id",
                keyValue: 19L);
        }
    }
}
