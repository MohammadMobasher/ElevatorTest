using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class change_Lenght_of_Title_Name_ProductUnit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "ProductUnit",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ProductUnit",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 10);

            migrationBuilder.AddColumn<int>(
                name: "ProductGroupId",
                table: "Product",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Product",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductGroupId",
                table: "Product",
                column: "ProductGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductGroup_ProductGroupId",
                table: "Product",
                column: "ProductGroupId",
                principalTable: "ProductGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductGroup_ProductGroupId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_ProductGroupId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ProductGroupId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Product");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "ProductUnit",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ProductUnit",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 40);
        }
    }
}
