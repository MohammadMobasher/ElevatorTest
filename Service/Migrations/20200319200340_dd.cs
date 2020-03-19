using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class dd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InstaURL",
                table: "SiteSetting",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TelegramURL",
                table: "SiteSetting",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TwitterURL",
                table: "SiteSetting",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WhatsAppURL",
                table: "SiteSetting",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InstaURL",
                table: "SiteSetting");

            migrationBuilder.DropColumn(
                name: "TelegramURL",
                table: "SiteSetting");

            migrationBuilder.DropColumn(
                name: "TwitterURL",
                table: "SiteSetting");

            migrationBuilder.DropColumn(
                name: "WhatsAppURL",
                table: "SiteSetting");
        }
    }
}
