using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class IsOnlinePayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOnlinePay",
                table: "ShopOrder",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOnlinePay",
                table: "ShopOrder");
        }
    }
}
