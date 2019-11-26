using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class RemoveUserID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UsersAccess");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "UsersAccess",
                nullable: false,
                defaultValue: 0);
        }
    }
}
