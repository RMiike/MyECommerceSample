using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MECS.Cart.API.Migrations
{
    public partial class cart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientCart",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdClient = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Total = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientCart", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItensCart",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdProduct = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Image = table.Column<string>(type: "varchar(100)", nullable: true),
                    IdCart = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItensCart", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItensCart_ClientCart_IdCart",
                        column: x => x.IdCart,
                        principalTable: "ClientCart",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IDX_Client",
                table: "ClientCart",
                column: "IdClient");

            migrationBuilder.CreateIndex(
                name: "IX_ItensCart_IdCart",
                table: "ItensCart",
                column: "IdCart");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItensCart");

            migrationBuilder.DropTable(
                name: "ClientCart");
        }
    }
}
