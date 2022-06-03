using Microsoft.EntityFrameworkCore.Migrations;

namespace MECS.Order.Infra.Migrations
{
    public partial class ordersAddressName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address_Numero",
                table: "Order",
                newName: "Numero");

            migrationBuilder.RenameColumn(
                name: "Address_Logradouro",
                table: "Order",
                newName: "Logradouro");

            migrationBuilder.RenameColumn(
                name: "Address_Estado",
                table: "Order",
                newName: "Estado");

            migrationBuilder.RenameColumn(
                name: "Address_Complemento",
                table: "Order",
                newName: "Complemento");

            migrationBuilder.RenameColumn(
                name: "Address_Cidade",
                table: "Order",
                newName: "Cidade");

            migrationBuilder.RenameColumn(
                name: "Address_CEP",
                table: "Order",
                newName: "CEP");

            migrationBuilder.RenameColumn(
                name: "Address_Bairro",
                table: "Order",
                newName: "Bairro");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Numero",
                table: "Order",
                newName: "Address_Numero");

            migrationBuilder.RenameColumn(
                name: "Logradouro",
                table: "Order",
                newName: "Address_Logradouro");

            migrationBuilder.RenameColumn(
                name: "Estado",
                table: "Order",
                newName: "Address_Estado");

            migrationBuilder.RenameColumn(
                name: "Complemento",
                table: "Order",
                newName: "Address_Complemento");

            migrationBuilder.RenameColumn(
                name: "Cidade",
                table: "Order",
                newName: "Address_Cidade");

            migrationBuilder.RenameColumn(
                name: "CEP",
                table: "Order",
                newName: "Address_CEP");

            migrationBuilder.RenameColumn(
                name: "Bairro",
                table: "Order",
                newName: "Address_Bairro");
        }
    }
}
