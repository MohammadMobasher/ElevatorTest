using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class productPackagePriceComputed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PackagePrice",
                table: "ProductPackage",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PackagePrice",
                table: "ProductPackage");
        }
    }
}
