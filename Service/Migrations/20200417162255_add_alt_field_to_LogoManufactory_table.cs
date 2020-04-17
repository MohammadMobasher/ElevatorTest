using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class add_alt_field_to_LogoManufactory_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Alt",
                table: "LogoManufactory",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Alt",
                table: "LogoManufactory");
        }
    }
}
