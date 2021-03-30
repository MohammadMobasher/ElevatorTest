using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class PhoneNumberAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNumberTo",
                table: "UserAddress",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumberTo",
                table: "UserAddress");
        }
    }
}
