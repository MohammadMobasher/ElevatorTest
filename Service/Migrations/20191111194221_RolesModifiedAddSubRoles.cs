using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class RolesModifiedAddSubRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RoleTItle",
                table: "AspNetRoles",
                newName: "RoleTitle");

            migrationBuilder.AddColumn<string>(
                name: "SubRoles",
                table: "AspNetRoles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubRoles",
                table: "AspNetRoles");

            migrationBuilder.RenameColumn(
                name: "RoleTitle",
                table: "AspNetRoles",
                newName: "RoleTItle");
        }
    }
}
