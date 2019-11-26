using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class UserRoleModify : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanDelete",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "CanInsert",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "CanRead",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "CanUpdate",
                table: "AspNetUserRoles");

            migrationBuilder.RenameColumn(
                name: "SubRoles",
                table: "AspNetUserRoles",
                newName: "SubAccess");

            migrationBuilder.AlterColumn<string>(
                name: "SummeryNews",
                table: "News",
                maxLength: 600,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 500);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "News",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Access",
                table: "AspNetUserRoles",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NewsComment_NewsId",
                table: "NewsComment",
                column: "NewsId");

            migrationBuilder.CreateIndex(
                name: "IX_News_UserId",
                table: "News",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_News_AspNetUsers_UserId",
                table: "News",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NewsComment_News_NewsId",
                table: "NewsComment",
                column: "NewsId",
                principalTable: "News",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_News_AspNetUsers_UserId",
                table: "News");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsComment_News_NewsId",
                table: "NewsComment");

            migrationBuilder.DropIndex(
                name: "IX_NewsComment_NewsId",
                table: "NewsComment");

            migrationBuilder.DropIndex(
                name: "IX_News_UserId",
                table: "News");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "News");

            migrationBuilder.DropColumn(
                name: "Access",
                table: "AspNetUserRoles");

            migrationBuilder.RenameColumn(
                name: "SubAccess",
                table: "AspNetUserRoles",
                newName: "SubRoles");

            migrationBuilder.AlterColumn<string>(
                name: "SummeryNews",
                table: "News",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 600);

            migrationBuilder.AddColumn<bool>(
                name: "CanDelete",
                table: "AspNetUserRoles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanInsert",
                table: "AspNetUserRoles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanRead",
                table: "AspNetUserRoles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanUpdate",
                table: "AspNetUserRoles",
                nullable: false,
                defaultValue: false);
        }
    }
}
