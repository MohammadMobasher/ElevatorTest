using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class ShopProductAddSomeField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrderName",
                table: "ShopProduct",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrderPrice",
                table: "ShopProduct",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrderPriceDiscount",
                table: "ShopProduct",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderName",
                table: "ShopProduct");

            migrationBuilder.DropColumn(
                name: "OrderPrice",
                table: "ShopProduct");

            migrationBuilder.DropColumn(
                name: "OrderPriceDiscount",
                table: "ShopProduct");
        }
    }
}
