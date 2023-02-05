using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Core.Migrations
{
    /// <inheritdoc />
    public partial class AuthRefactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "role_permissions",
                schema: "core");

            migrationBuilder.DropTable(
                name: "permissions",
                schema: "core");

            migrationBuilder.CreateTable(
                name: "user_roles",
                schema: "core",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    roleid = table.Column<long>(name: "role_id", type: "bigint", nullable: false),
                    userid = table.Column<long>(name: "user_id", type: "bigint", nullable: false),
                    updateuserid = table.Column<long>(name: "update_user_id", type: "bigint", nullable: false),
                    updatedat = table.Column<DateTime>(name: "updated_at", type: "timestamp with time zone", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_roles", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_roles_users_update_user_id",
                        column: x => x.updateuserid,
                        principalSchema: "core",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "user_roles_role_id_fkey",
                        column: x => x.roleid,
                        principalSchema: "core",
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "user_roles_user_id_fkey",
                        column: x => x.userid,
                        principalSchema: "core",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_user_roles_role_id",
                schema: "core",
                table: "user_roles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_roles_update_user_id",
                schema: "core",
                table: "user_roles",
                column: "update_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_roles_user_id",
                schema: "core",
                table: "user_roles",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_roles",
                schema: "core");

            migrationBuilder.CreateTable(
                name: "permissions",
                schema: "core",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    updateuserid = table.Column<long>(name: "update_user_id", type: "bigint", nullable: false),
                    name = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    updatedat = table.Column<DateTime>(name: "updated_at", type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_permissions", x => x.id);
                    table.ForeignKey(
                        name: "fk_permissions_users_update_user_id",
                        column: x => x.updateuserid,
                        principalSchema: "core",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "role_permissions",
                schema: "core",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    permissionid = table.Column<long>(name: "permission_id", type: "bigint", nullable: false),
                    roleid = table.Column<long>(name: "role_id", type: "bigint", nullable: false),
                    updateuserid = table.Column<long>(name: "update_user_id", type: "bigint", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    updatedat = table.Column<DateTime>(name: "updated_at", type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_role_permissions", x => x.id);
                    table.ForeignKey(
                        name: "fk_role_permissions_users_update_user_id",
                        column: x => x.updateuserid,
                        principalSchema: "core",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "role_permissions_permission_id_fkey",
                        column: x => x.permissionid,
                        principalSchema: "core",
                        principalTable: "permissions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "role_permissions_role_id_fkey",
                        column: x => x.roleid,
                        principalSchema: "core",
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_permissions_name",
                schema: "core",
                table: "permissions",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_permissions_update_user_id",
                schema: "core",
                table: "permissions",
                column: "update_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_role_permissions_permission_id",
                schema: "core",
                table: "role_permissions",
                column: "permission_id");

            migrationBuilder.CreateIndex(
                name: "ix_role_permissions_role_id",
                schema: "core",
                table: "role_permissions",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_role_permissions_update_user_id",
                schema: "core",
                table: "role_permissions",
                column: "update_user_id");
        }
    }
}
