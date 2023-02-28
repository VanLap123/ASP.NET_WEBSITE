using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WEBGROUP_GCC0903.Migrations
{
    public partial class Mig7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "order_id",
                table: "Customers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    order_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cus_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    deliveryLocal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cus_phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.order_id);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    order_id = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    pro_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => new { x.order_id, x.pro_id });
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_order_id",
                        column: x => x.order_id,
                        principalTable: "Orders",
                        principalColumn: "order_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Products_pro_id",
                        column: x => x.pro_id,
                        principalTable: "Products",
                        principalColumn: "pro_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_order_id",
                table: "Customers",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_pro_id",
                table: "OrderDetails",
                column: "pro_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Orders_order_id",
                table: "Customers",
                column: "order_id",
                principalTable: "Orders",
                principalColumn: "order_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Orders_order_id",
                table: "Customers");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Customers_order_id",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "order_id",
                table: "Customers");
        }
    }
}
