using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class Add_Shop_Order_Add_Field : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ShopOrder",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "ShopOrder");
        }
    }
}
