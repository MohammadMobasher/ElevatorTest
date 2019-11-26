using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class UsersRoleModify : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
