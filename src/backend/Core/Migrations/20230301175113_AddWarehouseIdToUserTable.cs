using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    /// <inheritdoc />
    public partial class AddWarehouseIdToUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_warehouses_users_update_user_id",
                schema: "core",
                table: "warehouses");

            migrationBuilder.AddColumn<long>(
                name: "warehouse_id",
                schema: "auth",
                table: "users",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_users_warehouse_id",
                schema: "auth",
                table: "users",
                column: "warehouse_id");

            migrationBuilder.AddForeignKey(
                name: "warehouse_users_id_fkey",
                schema: "auth",
                table: "users",
                column: "warehouse_id",
                principalSchema: "core",
                principalTable: "warehouses",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_warehouses_users_update_user_id1",
                schema: "core",
                table: "warehouses",
                column: "update_user_id",
                principalSchema: "auth",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "warehouse_users_id_fkey",
                schema: "auth",
                table: "users");

            migrationBuilder.DropForeignKey(
                name: "fk_warehouses_users_update_user_id1",
                schema: "core",
                table: "warehouses");

            migrationBuilder.DropIndex(
                name: "ix_users_warehouse_id",
                schema: "auth",
                table: "users");

            migrationBuilder.DropColumn(
                name: "warehouse_id",
                schema: "auth",
                table: "users");

            migrationBuilder.AddForeignKey(
                name: "fk_warehouses_users_update_user_id",
                schema: "core",
                table: "warehouses",
                column: "update_user_id",
                principalSchema: "auth",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
