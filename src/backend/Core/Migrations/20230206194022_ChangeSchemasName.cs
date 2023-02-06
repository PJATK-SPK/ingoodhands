using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    /// <inheritdoc />
    public partial class ChangeSchemasName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "auth");

            migrationBuilder.RenameTable(
                name: "users",
                schema: "core",
                newName: "users",
                newSchema: "auth");

            migrationBuilder.RenameTable(
                name: "user_roles",
                schema: "core",
                newName: "user_roles",
                newSchema: "auth");

            migrationBuilder.RenameTable(
                name: "roles",
                schema: "core",
                newName: "roles",
                newSchema: "auth");

            migrationBuilder.RenameTable(
                name: "auth0_users",
                schema: "core",
                newName: "auth0_users",
                newSchema: "auth");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "users",
                schema: "auth",
                newName: "users",
                newSchema: "core");

            migrationBuilder.RenameTable(
                name: "user_roles",
                schema: "auth",
                newName: "user_roles",
                newSchema: "core");

            migrationBuilder.RenameTable(
                name: "roles",
                schema: "auth",
                newName: "roles",
                newSchema: "core");

            migrationBuilder.RenameTable(
                name: "auth0_users",
                schema: "auth",
                newName: "auth0_users",
                newSchema: "core");
        }
    }
}
