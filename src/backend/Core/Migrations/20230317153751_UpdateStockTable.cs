using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    /// <inheritdoc />
    public partial class UpdateStockTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "warehouse_id",
                schema: "core",
                table: "stocks",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "ix_stocks_warehouse_id",
                schema: "core",
                table: "stocks",
                column: "warehouse_id");

            migrationBuilder.AddForeignKey(
                name: "stocks_warehouse_id_fkey",
                schema: "core",
                table: "stocks",
                column: "warehouse_id",
                principalSchema: "core",
                principalTable: "warehouses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "stocks_warehouse_id_fkey",
                schema: "core",
                table: "stocks");

            migrationBuilder.DropIndex(
                name: "ix_stocks_warehouse_id",
                schema: "core",
                table: "stocks");

            migrationBuilder.DropColumn(
                name: "warehouse_id",
                schema: "core",
                table: "stocks");
        }
    }
}
