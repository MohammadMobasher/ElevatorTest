using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class ShopProductmodify : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ShopProduct",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "PackageId",
                table: "ShopProduct",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PackageId",
                table: "ShopProduct");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ShopProduct",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
