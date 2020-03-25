using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class remove_title_and_alt_from_slideShow_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Alt",
                table: "SlideShow");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "SlideShow");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Alt",
                table: "SlideShow",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "SlideShow",
                maxLength: 100,
                nullable: true);
        }
    }
}
