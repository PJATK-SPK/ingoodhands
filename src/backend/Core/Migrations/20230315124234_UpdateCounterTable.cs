using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Core.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCounterTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "core",
                table: "counters",
                columns: new[] { "id", "name", "status", "update_user_id", "updated_at", "value" },
                values: new object[,]
                {
                    { 2L, "Orders", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0L },
                    { 3L, "Deliveries", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0L }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "core",
                table: "counters",
                keyColumn: "id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "counters",
                keyColumn: "id",
                keyValue: 3L);
        }
    }
}
