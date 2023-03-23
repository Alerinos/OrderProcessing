using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderProcessing.Server.Migrations
{
    /// <inheritdoc />
    public partial class OrderProcessing_v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Order.Order",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order.Order", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Order.OrderLine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Product = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order.OrderLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order.OrderLine_Order.Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order.Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order.OrderLine_OrderId",
                table: "Order.OrderLine",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Order.OrderLine");

            migrationBuilder.DropTable(
                name: "Order.Order");
        }
    }
}
