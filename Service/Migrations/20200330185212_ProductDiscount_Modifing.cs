using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class ProductDiscount_Modifing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsArchive",
                table: "ProductDiscount",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsArchive",
                table: "ProductDiscount");
        }
    }
}
