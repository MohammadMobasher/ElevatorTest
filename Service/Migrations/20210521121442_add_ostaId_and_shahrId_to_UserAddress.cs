using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class add_ostaId_and_shahrId_to_UserAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OstanId",
                table: "UserAddress",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShahrId",
                table: "UserAddress",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAddress_OstanId",
                table: "UserAddress",
                column: "OstanId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAddress_ShahrId",
                table: "UserAddress",
                column: "ShahrId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAddress_Ostan_OstanId",
                table: "UserAddress",
                column: "OstanId",
                principalTable: "Ostan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAddress_Shahr_ShahrId",
                table: "UserAddress",
                column: "ShahrId",
                principalTable: "Shahr",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAddress_Ostan_OstanId",
                table: "UserAddress");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAddress_Shahr_ShahrId",
                table: "UserAddress");

            migrationBuilder.DropIndex(
                name: "IX_UserAddress_OstanId",
                table: "UserAddress");

            migrationBuilder.DropIndex(
                name: "IX_UserAddress_ShahrId",
                table: "UserAddress");

            migrationBuilder.DropColumn(
                name: "OstanId",
                table: "UserAddress");

            migrationBuilder.DropColumn(
                name: "ShahrId",
                table: "UserAddress");
        }
    }
}
