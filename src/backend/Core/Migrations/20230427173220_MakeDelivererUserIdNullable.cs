using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    /// <inheritdoc />
    public partial class MakeDelivererUserIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "deliveries_deliverer_users_id_fkey",
                schema: "core",
                table: "deliveries");

            migrationBuilder.AlterColumn<long>(
                name: "deliverer_user_id",
                schema: "core",
                table: "deliveries",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "deliveries_deliverer_users_id_fkey",
                schema: "core",
                table: "deliveries",
                column: "deliverer_user_id",
                principalSchema: "auth",
                principalTable: "users",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "deliveries_deliverer_users_id_fkey",
                schema: "core",
                table: "deliveries");

            migrationBuilder.AlterColumn<long>(
                name: "deliverer_user_id",
                schema: "core",
                table: "deliveries",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "deliveries_deliverer_users_id_fkey",
                schema: "core",
                table: "deliveries",
                column: "deliverer_user_id",
                principalSchema: "auth",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
