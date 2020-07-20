using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class RemoveShopOrderRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersPayment_ShopOrder_ShopOrderId",
                table: "UsersPayment");

            migrationBuilder.DropIndex(
                name: "IX_UsersPayment_ShopOrderId",
                table: "UsersPayment");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UsersPayment_ShopOrderId",
                table: "UsersPayment",
                column: "ShopOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersPayment_ShopOrder_ShopOrderId",
                table: "UsersPayment",
                column: "ShopOrderId",
                principalTable: "ShopOrder",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
