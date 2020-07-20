using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class ShopProduct_AddIsFactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OrderId",
                table: "ShopOrderPayment",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFactorSubmited",
                table: "Product",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFactorSubmited",
                table: "Product");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "ShopOrderPayment",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
