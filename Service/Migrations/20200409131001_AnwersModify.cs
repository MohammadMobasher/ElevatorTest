using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class AnwersModify : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PackageId",
                table: "PackageUserAnswers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PackageUserAnswers_PackageId",
                table: "PackageUserAnswers",
                column: "PackageId");

            migrationBuilder.AddForeignKey(
                name: "FK_PackageUserAnswers_ProductPackage_PackageId",
                table: "PackageUserAnswers",
                column: "PackageId",
                principalTable: "ProductPackage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PackageUserAnswers_ProductPackage_PackageId",
                table: "PackageUserAnswers");

            migrationBuilder.DropIndex(
                name: "IX_PackageUserAnswers_PackageId",
                table: "PackageUserAnswers");

            migrationBuilder.DropColumn(
                name: "PackageId",
                table: "PackageUserAnswers");
        }
    }
}
