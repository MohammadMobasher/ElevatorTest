using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class dddd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "ProductGroup",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductGroup_ParentId",
                table: "ProductGroup",
                column: "ParentId");

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

            migrationBuilder.DropIndex(
                name: "IX_ProductGroup_ParentId",
                table: "ProductGroup");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "ProductGroup");
        }
    }
}
