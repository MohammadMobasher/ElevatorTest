using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class add_Field_NewsGroupId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "UsersAccess",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NewsGroupId",
                table: "News",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_News_NewsGroupId",
                table: "News",
                column: "NewsGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_News_NewsGroup_NewsGroupId",
                table: "News",
                column: "NewsGroupId",
                principalTable: "NewsGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_News_NewsGroup_NewsGroupId",
                table: "News");

            migrationBuilder.DropIndex(
                name: "IX_News_NewsGroupId",
                table: "News");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UsersAccess");

            migrationBuilder.DropColumn(
                name: "NewsGroupId",
                table: "News");
        }
    }
}
