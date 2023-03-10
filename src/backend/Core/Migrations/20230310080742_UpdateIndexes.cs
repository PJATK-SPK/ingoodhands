using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    /// <inheritdoc />
    public partial class UpdateIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "percentage_idx",
                schema: "core",
                table: "orders");

            migrationBuilder.CreateIndex(
                name: "order_name_idx",
                schema: "core",
                table: "orders",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "percentage_idx",
                schema: "core",
                table: "orders",
                column: "percentage");

            migrationBuilder.CreateIndex(
                name: "donation_name_idx",
                schema: "core",
                table: "donations",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "delivery_name_idx",
                schema: "core",
                table: "deliveries",
                column: "name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "order_name_idx",
                schema: "core",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "percentage_idx",
                schema: "core",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "donation_name_idx",
                schema: "core",
                table: "donations");

            migrationBuilder.DropIndex(
                name: "delivery_name_idx",
                schema: "core",
                table: "deliveries");

            migrationBuilder.CreateIndex(
                name: "percentage_idx",
                schema: "core",
                table: "orders",
                column: "percentage",
                unique: true);
        }
    }
}
