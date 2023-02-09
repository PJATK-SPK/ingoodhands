using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Core.Migrations
{
    /// <inheritdoc />
    public partial class AddWarehouseData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "core",
                table: "addresses",
                columns: new[] { "id", "apartment", "city", "country_id", "gps_latitude", "gps_longitude", "postal_code", "status", "street", "street_number", "update_user_id", "updated_at" },
                values: new object[,]
                {
                    { 1L, null, "Gdansk", 177L, 54.406727436950163, 18.626590482862696, "80-503", 0, "al. gen. J. Hallera", "239", 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2L, "22", "Krakow", 177L, 50.050510416252955, 19.967383557975161, "30-701", 0, "ul. Zablocie", "20", 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 3L, null, "Piastow", 177L, 52.180876508945325, 20.864602669100716, "05-820", 0, "ul. St. Bodycha", "97", 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 4L, null, "Berlin", 84L, 52.624374124511696, 13.337297904503853, "13435", 0, "Wallenroder Strasse", "7", 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 5L, null, "Munich", 84L, 48.141578533011334, 11.648666754927092, "81677", 0, "Kronstadter Str", "30", 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 6L, null, "Budapest", 101L, 47.469941092986005, 19.109420384400256, "1097", 0, "Feherakac u.", "821", 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 7L, "40", "Prague", 61L, 50.124658931700118, 14.439680032281526, "17000", 0, "Argentinska", "516", 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 8L, null, "Nice", 77L, 43.666140937587961, 7.2026324199422023, "06200", 0, "Pass. du Fret", null, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 9L, null, "Berlin", 77L, 52.624374124511696, 13.337297904503853, "13435", 0, "Wallenroder Strasse", "7", 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                schema: "core",
                table: "warehouses",
                columns: new[] { "id", "address_id", "short_name", "status", "update_user_id", "updated_at" },
                values: new object[,]
                {
                    { 1L, 1L, "PL001", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2L, 2L, "PL002", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 3L, 3L, "PL003", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 4L, 4L, "DE001", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 5L, 5L, "DE002", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 6L, 6L, "HU001", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 7L, 7L, "CZ001", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 8L, 8L, "FR001", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 9L, 9L, "FR002", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "core",
                table: "warehouses",
                keyColumn: "id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "warehouses",
                keyColumn: "id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "warehouses",
                keyColumn: "id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "warehouses",
                keyColumn: "id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "warehouses",
                keyColumn: "id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "warehouses",
                keyColumn: "id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "warehouses",
                keyColumn: "id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "warehouses",
                keyColumn: "id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "warehouses",
                keyColumn: "id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "addresses",
                keyColumn: "id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "addresses",
                keyColumn: "id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "addresses",
                keyColumn: "id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "addresses",
                keyColumn: "id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "addresses",
                keyColumn: "id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "addresses",
                keyColumn: "id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "addresses",
                keyColumn: "id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "addresses",
                keyColumn: "id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                schema: "core",
                table: "addresses",
                keyColumn: "id",
                keyValue: 9L);
        }
    }
}
