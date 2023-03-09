using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Core.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderAndDeliveryTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_stocks_products_product_id",
                schema: "core",
                table: "stocks");

            migrationBuilder.CreateTable(
                name: "orders",
                schema: "core",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    address_id = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "character varying(9)", maxLength: 9, nullable: false),
                    percentage = table.Column<int>(type: "integer", nullable: false),
                    owner_user_id = table.Column<long>(type: "bigint", nullable: false),
                    creation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_canceled_by_user = table.Column<bool>(type: "boolean", nullable: false),
                    update_user_id = table.Column<long>(type: "bigint", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_orders", x => x.id);
                    table.ForeignKey(
                        name: "address_orders_id_fkey",
                        column: x => x.address_id,
                        principalSchema: "core",
                        principalTable: "addresses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_orders_users_update_user_id",
                        column: x => x.update_user_id,
                        principalSchema: "auth",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "orders_owner_users_id_fkey",
                        column: x => x.owner_user_id,
                        principalSchema: "auth",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "deliveries",
                schema: "core",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(9)", maxLength: 9, nullable: false),
                    order_id = table.Column<long>(type: "bigint", nullable: false),
                    is_delivered = table.Column<bool>(type: "boolean", nullable: false),
                    deliverer_user_id = table.Column<long>(type: "bigint", nullable: false),
                    creation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_lost = table.Column<bool>(type: "boolean", nullable: false),
                    is_packed = table.Column<bool>(type: "boolean", nullable: false),
                    warehouse_id = table.Column<long>(type: "bigint", nullable: false),
                    update_user_id = table.Column<long>(type: "bigint", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_deliveries", x => x.id);
                    table.ForeignKey(
                        name: "deliveries_deliverer_users_id_fkey",
                        column: x => x.deliverer_user_id,
                        principalSchema: "auth",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "deliveries_warehouses_id_fkey",
                        column: x => x.warehouse_id,
                        principalSchema: "core",
                        principalTable: "warehouses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_deliveries_orders_order_id",
                        column: x => x.order_id,
                        principalSchema: "core",
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_deliveries_users_update_user_id",
                        column: x => x.update_user_id,
                        principalSchema: "auth",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order_products",
                schema: "core",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    order_id = table.Column<long>(type: "bigint", nullable: false),
                    product_id = table.Column<long>(type: "bigint", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    update_user_id = table.Column<long>(type: "bigint", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order_products", x => x.id);
                    table.ForeignKey(
                        name: "fk_order_products_users_update_user_id",
                        column: x => x.update_user_id,
                        principalSchema: "auth",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "order_products_id_fkey",
                        column: x => x.order_id,
                        principalSchema: "core",
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "product_orders_id_fkey",
                        column: x => x.product_id,
                        principalSchema: "core",
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "delivery_products",
                schema: "core",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    delivery_id = table.Column<long>(type: "bigint", nullable: false),
                    product_id = table.Column<long>(type: "bigint", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    update_user_id = table.Column<long>(type: "bigint", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_delivery_products", x => x.id);
                    table.ForeignKey(
                        name: "delivery_products_id_fkey",
                        column: x => x.delivery_id,
                        principalSchema: "core",
                        principalTable: "deliveries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_delivery_products_users_update_user_id",
                        column: x => x.update_user_id,
                        principalSchema: "auth",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "product_deliveries_id_fkey",
                        column: x => x.product_id,
                        principalSchema: "core",
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_deliveries_deliverer_user_id",
                schema: "core",
                table: "deliveries",
                column: "deliverer_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_deliveries_order_id",
                schema: "core",
                table: "deliveries",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "ix_deliveries_update_user_id",
                schema: "core",
                table: "deliveries",
                column: "update_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_deliveries_warehouse_id",
                schema: "core",
                table: "deliveries",
                column: "warehouse_id");

            migrationBuilder.CreateIndex(
                name: "ix_delivery_products_delivery_id",
                schema: "core",
                table: "delivery_products",
                column: "delivery_id");

            migrationBuilder.CreateIndex(
                name: "ix_delivery_products_product_id",
                schema: "core",
                table: "delivery_products",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_delivery_products_update_user_id",
                schema: "core",
                table: "delivery_products",
                column: "update_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_products_order_id",
                schema: "core",
                table: "order_products",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_products_product_id",
                schema: "core",
                table: "order_products",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_products_update_user_id",
                schema: "core",
                table: "order_products",
                column: "update_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_orders_address_id",
                schema: "core",
                table: "orders",
                column: "address_id");

            migrationBuilder.CreateIndex(
                name: "ix_orders_owner_user_id",
                schema: "core",
                table: "orders",
                column: "owner_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_orders_update_user_id",
                schema: "core",
                table: "orders",
                column: "update_user_id");

            migrationBuilder.CreateIndex(
                name: "percentage_idx",
                schema: "core",
                table: "orders",
                column: "percentage",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "stocks_product_id_fkey",
                schema: "core",
                table: "stocks",
                column: "product_id",
                principalSchema: "core",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "stocks_product_id_fkey",
                schema: "core",
                table: "stocks");

            migrationBuilder.DropTable(
                name: "delivery_products",
                schema: "core");

            migrationBuilder.DropTable(
                name: "order_products",
                schema: "core");

            migrationBuilder.DropTable(
                name: "deliveries",
                schema: "core");

            migrationBuilder.DropTable(
                name: "orders",
                schema: "core");

            migrationBuilder.AddForeignKey(
                name: "fk_stocks_products_product_id",
                schema: "core",
                table: "stocks",
                column: "product_id",
                principalSchema: "core",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
