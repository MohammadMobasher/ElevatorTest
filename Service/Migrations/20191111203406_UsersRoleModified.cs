using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class UsersRoleModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubRoles",
                table: "AspNetRoles");

            migrationBuilder.AddColumn<string>(
                name: "SubRoles",
                table: "AspNetUserRoles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubRoles",
                table: "AspNetUserRoles");

            migrationBuilder.AddColumn<string>(
                name: "SubRoles",
                table: "AspNetRoles",
                nullable: true);
        }
    }
}
