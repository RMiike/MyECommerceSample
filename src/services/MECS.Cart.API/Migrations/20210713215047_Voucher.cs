using Microsoft.EntityFrameworkCore.Migrations;

namespace MECS.Cart.API.Migrations
{
    public partial class Voucher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Descount",
                table: "ClientCart",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsedVoucher",
                table: "ClientCart",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "Percentual",
                table: "ClientCart",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TipoDesconto",
                table: "ClientCart",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorDesconto",
                table: "ClientCart",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VoucherCodigo",
                table: "ClientCart",
                type: "varchar(50)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descount",
                table: "ClientCart");

            migrationBuilder.DropColumn(
                name: "IsUsedVoucher",
                table: "ClientCart");

            migrationBuilder.DropColumn(
                name: "Percentual",
                table: "ClientCart");

            migrationBuilder.DropColumn(
                name: "TipoDesconto",
                table: "ClientCart");

            migrationBuilder.DropColumn(
                name: "ValorDesconto",
                table: "ClientCart");

            migrationBuilder.DropColumn(
                name: "VoucherCodigo",
                table: "ClientCart");
        }
    }
}
