using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    /// <inheritdoc />
    public partial class FixFR002C : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "core",
                table: "addresses",
                keyColumn: "id",
                keyValue: 9L,
                columns: new[] { "city", "gps_latitude", "gps_longitude", "postal_code", "street", "street_number" },
                values: new object[] { "Méry-sur-Oise", 49.064450262299367, 2.1857450490282759, "95540", "Imp. du Château", "2" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "core",
                table: "addresses",
                keyColumn: "id",
                keyValue: 9L,
                columns: new[] { "city", "gps_latitude", "gps_longitude", "postal_code", "street", "street_number" },
                values: new object[] { "Berlin", 52.624374124511696, 13.337297904503853, "13435", "Wallenroder Strasse", "7" });
        }
    }
}
