using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class IsOnlinePaymentForShopOrderPayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOnlinePay",
                table: "ShopOrderPayment",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOnlinePay",
                table: "ShopOrderPayment");
        }
    }
}
