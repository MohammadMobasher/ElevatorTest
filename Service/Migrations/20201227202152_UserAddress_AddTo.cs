using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class UserAddress_AddTo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "To",
                table: "UserAddress",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "To",
                table: "UserAddress");
        }
    }
}
