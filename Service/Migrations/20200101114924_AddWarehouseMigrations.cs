using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class AddWarehouseMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Warehouse_Product_ProductId",
                table: "Warehouse");

            migrationBuilder.DropIndex(
                name: "IX_Warehouse_ProductId",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Warehouse");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Warehouse",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Warehouse_ProductId",
                table: "Warehouse",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouse_Product_ProductId",
                table: "Warehouse",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
