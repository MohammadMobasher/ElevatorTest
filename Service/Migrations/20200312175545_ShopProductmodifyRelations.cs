using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class ShopProductmodifyRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ShopProduct_PackageId",
                table: "ShopProduct",
                column: "PackageId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopProduct_ProductId",
                table: "ShopProduct",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShopProduct_ProductPackage_PackageId",
                table: "ShopProduct",
                column: "PackageId",
                principalTable: "ProductPackage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShopProduct_Product_ProductId",
                table: "ShopProduct",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShopProduct_ProductPackage_PackageId",
                table: "ShopProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_ShopProduct_Product_ProductId",
                table: "ShopProduct");

            migrationBuilder.DropIndex(
                name: "IX_ShopProduct_PackageId",
                table: "ShopProduct");

            migrationBuilder.DropIndex(
                name: "IX_ShopProduct_ProductId",
                table: "ShopProduct");
        }
    }
}
