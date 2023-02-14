using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Core.Migrations
{
    /// <inheritdoc />
    public partial class AddDonationDonationProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "donations",
                schema: "core",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    creationuserid = table.Column<long>(name: "creation_user_id", type: "bigint", nullable: false),
                    creationdate = table.Column<DateTime>(name: "creation_date", type: "timestamp with time zone", nullable: false),
                    warehouseid = table.Column<long>(name: "warehouse_id", type: "bigint", nullable: false),
                    name = table.Column<string>(type: "character varying(9)", maxLength: 9, nullable: false),
                    isexpired = table.Column<bool>(name: "is_expired", type: "boolean", nullable: false),
                    isdelivered = table.Column<bool>(name: "is_delivered", type: "boolean", nullable: false),
                    isincludedinstock = table.Column<bool>(name: "is_included_in_stock", type: "boolean", nullable: false),
                    updateuserid = table.Column<long>(name: "update_user_id", type: "bigint", nullable: false),
                    updatedat = table.Column<DateTime>(name: "updated_at", type: "timestamp with time zone", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_donations", x => x.id);
                    table.ForeignKey(
                        name: "donations_creation_users_id_fkey",
                        column: x => x.creationuserid,
                        principalSchema: "auth",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "donations_warehouses_id_fkey",
                        column: x => x.warehouseid,
                        principalSchema: "core",
                        principalTable: "warehouses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_donations_users_update_user_id",
                        column: x => x.updateuserid,
                        principalSchema: "auth",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "donation_products",
                schema: "core",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    donationid = table.Column<long>(name: "donation_id", type: "bigint", nullable: false),
                    productid = table.Column<long>(name: "product_id", type: "bigint", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    updateuserid = table.Column<long>(name: "update_user_id", type: "bigint", nullable: false),
                    updatedat = table.Column<DateTime>(name: "updated_at", type: "timestamp with time zone", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_donation_products", x => x.id);
                    table.ForeignKey(
                        name: "donations_products_id_fkey",
                        column: x => x.donationid,
                        principalSchema: "core",
                        principalTable: "donations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_donation_products_users_update_user_id",
                        column: x => x.updateuserid,
                        principalSchema: "auth",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "product_donations_id_fkey",
                        column: x => x.productid,
                        principalSchema: "core",
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_donation_products_donation_id",
                schema: "core",
                table: "donation_products",
                column: "donation_id");

            migrationBuilder.CreateIndex(
                name: "ix_donation_products_product_id",
                schema: "core",
                table: "donation_products",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_donation_products_update_user_id",
                schema: "core",
                table: "donation_products",
                column: "update_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_donations_creation_user_id",
                schema: "core",
                table: "donations",
                column: "creation_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_donations_update_user_id",
                schema: "core",
                table: "donations",
                column: "update_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_donations_warehouse_id",
                schema: "core",
                table: "donations",
                column: "warehouse_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "donation_products",
                schema: "core");

            migrationBuilder.DropTable(
                name: "donations",
                schema: "core");
        }
    }
}
