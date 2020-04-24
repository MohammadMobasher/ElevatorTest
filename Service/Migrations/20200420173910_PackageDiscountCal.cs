using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class PackageDiscountCal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PackageWithDiscounts",
                table: "ProductPackage",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PackageWithDiscounts",
                table: "ProductPackage");
        }
    }
}
