using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrderDeliveryRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_deliveries_orders_order_id",
                schema: "core",
                table: "deliveries");

            migrationBuilder.AddForeignKey(
                name: "deliveries_order_id_fkey",
                schema: "core",
                table: "deliveries",
                column: "order_id",
                principalSchema: "core",
                principalTable: "orders",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "deliveries_order_id_fkey",
                schema: "core",
                table: "deliveries");

            migrationBuilder.AddForeignKey(
                name: "fk_deliveries_orders_order_id",
                schema: "core",
                table: "deliveries",
                column: "order_id",
                principalSchema: "core",
                principalTable: "orders",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
