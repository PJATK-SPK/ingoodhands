using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Core.Migrations
{
    /// <inheritdoc />
    public partial class AddUserAddressesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user_addresses",
                schema: "core",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    address_id = table.Column<long>(type: "bigint", nullable: false),
                    is_deleted_by_user = table.Column<bool>(type: "boolean", nullable: false),
                    update_user_id = table.Column<long>(type: "bigint", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_addresses", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_addresses_addresses_address_id",
                        column: x => x.address_id,
                        principalSchema: "core",
                        principalTable: "addresses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_addresses_users_update_user_id",
                        column: x => x.update_user_id,
                        principalSchema: "auth",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "user_addresses_user_id_fkey",
                        column: x => x.user_id,
                        principalSchema: "auth",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_user_addresses_address_id",
                schema: "core",
                table: "user_addresses",
                column: "address_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_addresses_update_user_id",
                schema: "core",
                table: "user_addresses",
                column: "update_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_addresses_user_id",
                schema: "core",
                table: "user_addresses",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_addresses",
                schema: "core");
        }
    }
}
