using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class fffff1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductGroup_ProductGroup_ParentId",
                table: "ProductGroup");

            migrationBuilder.AlterColumn<int>(
                name: "ParentId",
                table: "ProductGroup",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_ProductGroup_ProductGroup_ParentId",
                table: "ProductGroup",
                column: "ParentId",
                principalTable: "ProductGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductGroup_ProductGroup_ParentId",
                table: "ProductGroup");

            migrationBuilder.AlterColumn<int>(
                name: "ParentId",
                table: "ProductGroup",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductGroup_ProductGroup_ParentId",
                table: "ProductGroup",
                column: "ParentId",
                principalTable: "ProductGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
