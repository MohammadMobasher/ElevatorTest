using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class UserRolesModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Access",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "SubAccess",
                table: "AspNetUserRoles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Access",
                table: "AspNetUserRoles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubAccess",
                table: "AspNetUserRoles",
                nullable: true);
        }
    }
}
