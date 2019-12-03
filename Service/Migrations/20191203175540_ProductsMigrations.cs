using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class ProductsMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductUnit_ProductUnitId",
                table: "Product");

            migrationBuilder.AlterColumn<int>(
                name: "ProductUnitId",
                table: "Product",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductUnit_ProductUnitId",
                table: "Product",
                column: "ProductUnitId",
                principalTable: "ProductUnit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductUnit_ProductUnitId",
                table: "Product");

            migrationBuilder.AlterColumn<int>(
                name: "ProductUnitId",
                table: "Product",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductUnit_ProductUnitId",
                table: "Product",
                column: "ProductUnitId",
                principalTable: "ProductUnit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
