using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class PackageDiscountMigeations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "PackageWithDiscounts",
                table: "ProductPackage",
                nullable: true,
                oldClrType: typeof(long));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "PackageWithDiscounts",
                table: "ProductPackage",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);
        }
    }
}
