using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    /// <inheritdoc />
    public partial class FixWarehouse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "core",
                table: "warehouses",
                keyColumn: "id",
                keyValue: 4L);

            migrationBuilder.UpdateData(
                schema: "core",
                table: "warehouses",
                keyColumn: "id",
                keyValue: 5L,
                columns: new[] { "address_id", "short_name" },
                values: new object[] { 4L, "DE001" });

            migrationBuilder.UpdateData(
                schema: "core",
                table: "warehouses",
                keyColumn: "id",
                keyValue: 9L,
                columns: new[] { "address_id", "short_name" },
                values: new object[] { 5L, "DE002" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "core",
                table: "warehouses",
                keyColumn: "id",
                keyValue: 5L,
                columns: new[] { "address_id", "short_name" },
                values: new object[] { 5L, "DE002" });

            migrationBuilder.UpdateData(
                schema: "core",
                table: "warehouses",
                keyColumn: "id",
                keyValue: 9L,
                columns: new[] { "address_id", "short_name" },
                values: new object[] { 9L, "FR002" });

            migrationBuilder.InsertData(
                schema: "core",
                table: "warehouses",
                columns: new[] { "id", "address_id", "short_name", "status", "update_user_id", "updated_at" },
                values: new object[] { 4L, 4L, "DE001", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });
        }
    }
}
