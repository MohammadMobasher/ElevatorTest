using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class RoleModify : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RoleTItle",
                table: "AspNetRoles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoleTItle",
                table: "AspNetRoles");
        }
    }
}
