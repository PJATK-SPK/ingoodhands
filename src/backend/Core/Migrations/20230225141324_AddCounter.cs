using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Core.Migrations
{
    /// <inheritdoc />
    public partial class AddCounter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "counters",
                schema: "core",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    value = table.Column<long>(type: "bigint", nullable: false),
                    update_user_id = table.Column<long>(type: "bigint", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_counters", x => x.id);
                    table.ForeignKey(
                        name: "fk_counters_users_update_user_id",
                        column: x => x.update_user_id,
                        principalSchema: "auth",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "core",
                table: "counters",
                columns: new[] { "id", "name", "status", "update_user_id", "updated_at", "value" },
                values: new object[] { 1L, "Donations", 0, 1L, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0L });

            migrationBuilder.CreateIndex(
                name: "ix_counters_name",
                schema: "core",
                table: "counters",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_counters_update_user_id",
                schema: "core",
                table: "counters",
                column: "update_user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "counters",
                schema: "core");
        }
    }
}
