using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class ProductPackageDetailAddProdctGroup_Field : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductGroupId",
                table: "ProductPackageDetails",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductGroupId",
                table: "ProductPackageDetails");
        }
    }
}
