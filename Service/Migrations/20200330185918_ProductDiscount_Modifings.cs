using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class ProductDiscount_Modifings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ProductDiscount",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ProductDiscount");
        }
    }
}
