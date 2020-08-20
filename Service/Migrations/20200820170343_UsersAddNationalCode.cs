using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class UsersAddNationalCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NationalCode",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NationalCode",
                table: "AspNetUsers");
        }
    }
}
