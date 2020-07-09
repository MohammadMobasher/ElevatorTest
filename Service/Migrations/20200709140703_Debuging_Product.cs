using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class Debuging_Product : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductSize",
                table: "TransportationTariff");

            migrationBuilder.AddColumn<double>(
                name: "ProductSizeFrom",
                table: "TransportationTariff",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ProductSizeTo",
                table: "TransportationTariff",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<double>(
                name: "ProductSize",
                table: "Product",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductSizeFrom",
                table: "TransportationTariff");

            migrationBuilder.DropColumn(
                name: "ProductSizeTo",
                table: "TransportationTariff");

            migrationBuilder.AddColumn<int>(
                name: "ProductSize",
                table: "TransportationTariff",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "ProductSize",
                table: "Product",
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);
        }
    }
}
