using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Core.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "core");

            migrationBuilder.CreateTable(
                name: "users",
                schema: "core",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    first_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    email = table.Column<string>(type: "character varying(254)", maxLength: 254, nullable: false),
                    locale = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    update_user_id = table.Column<long>(type: "bigint", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                    table.ForeignKey(
                        name: "fk_users_users_update_user_id",
                        column: x => x.update_user_id,
                        principalSchema: "core",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "auth_users",
                schema: "core",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    first_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    nickname = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "character varying(254)", maxLength: 254, nullable: false),
                    identifier = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    update_user_id = table.Column<long>(type: "bigint", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_auth_users", x => x.id);
                    table.ForeignKey(
                        name: "auth_users_user_id_fkey",
                        column: x => x.user_id,
                        principalSchema: "core",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_auth_users_users_update_user_id",
                        column: x => x.update_user_id,
                        principalSchema: "core",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "permissions",
                schema: "core",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    topic = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    update_user_id = table.Column<long>(type: "bigint", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_permissions", x => x.id);
                    table.ForeignKey(
                        name: "fk_permissions_users_update_user_id",
                        column: x => x.update_user_id,
                        principalSchema: "core",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "auth_users_status_identifier_idx",
                schema: "core",
                table: "auth_users",
                columns: new[] { "status", "identifier" });

            migrationBuilder.CreateIndex(
                name: "ix_auth_users_identifier",
                schema: "core",
                table: "auth_users",
                column: "identifier",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_auth_users_update_user_id",
                schema: "core",
                table: "auth_users",
                column: "update_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_auth_users_user_id",
                schema: "core",
                table: "auth_users",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_permissions_topic",
                schema: "core",
                table: "permissions",
                column: "topic",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_permissions_update_user_id",
                schema: "core",
                table: "permissions",
                column: "update_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_update_user_id",
                schema: "core",
                table: "users",
                column: "update_user_id");

            migrationBuilder.CreateIndex(
                name: "users_email_idx",
                schema: "core",
                table: "users",
                column: "email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "auth_users",
                schema: "core");

            migrationBuilder.DropTable(
                name: "permissions",
                schema: "core");

            migrationBuilder.DropTable(
                name: "users",
                schema: "core");
        }
    }
}
