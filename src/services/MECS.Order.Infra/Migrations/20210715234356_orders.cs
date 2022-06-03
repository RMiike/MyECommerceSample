using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MECS.Order.Infra.Migrations
{
    public partial class orders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "MinhaSequencia",
                startValue: 1000L);

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Codigo = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR MinhaSequencia"),
                    IdClient = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdVoucher = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsUsedVoucher = table.Column<bool>(type: "bit", nullable: false),
                    Descount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InitialDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderStatus = table.Column<int>(type: "int", nullable: false),
                    Address_Logradouro = table.Column<string>(type: "varchar(200)", nullable: true),
                    Address_Numero = table.Column<string>(type: "varchar(50)", nullable: true),
                    Address_Complemento = table.Column<string>(type: "varchar(250)", nullable: true),
                    Address_Bairro = table.Column<string>(type: "varchar(100)", nullable: true),
                    Address_CEP = table.Column<string>(type: "varchar(20)", nullable: true),
                    Address_Cidade = table.Column<string>(type: "varchar(100)", nullable: true),
                    Address_Estado = table.Column<string>(type: "varchar(50)", nullable: true),
                    VoucherId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Vouchers_VoucherId",
                        column: x => x.VoucherId,
                        principalTable: "Vouchers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdOrder = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdProduct = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductName = table.Column<string>(type: "varchar(250)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductImage = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItem_Order_IdOrder",
                        column: x => x.IdOrder,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_VoucherId",
                table: "Order",
                column: "VoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_IdOrder",
                table: "OrderItem",
                column: "IdOrder");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropSequence(
                name: "MinhaSequencia");
        }
    }
}
