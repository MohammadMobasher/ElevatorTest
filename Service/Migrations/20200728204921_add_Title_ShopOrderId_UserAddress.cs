using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class add_Title_ShopOrderId_UserAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShopOrderId",
                table: "UserAddress",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAddress_ShopOrderId",
                table: "UserAddress",
                column: "ShopOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAddress_ShopOrder_ShopOrderId",
                table: "UserAddress",
                column: "ShopOrderId",
                principalTable: "ShopOrder",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAddress_ShopOrder_ShopOrderId",
                table: "UserAddress");

            migrationBuilder.DropIndex(
                name: "IX_UserAddress_ShopOrderId",
                table: "UserAddress");

            migrationBuilder.DropColumn(
                name: "ShopOrderId",
                table: "UserAddress");
        }
    }
}
