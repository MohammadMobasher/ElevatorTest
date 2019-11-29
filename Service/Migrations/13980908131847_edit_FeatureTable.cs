using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class edit_FeatureTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Feature");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Feature",
                maxLength: 150,
                nullable: false,
                defaultValue: "");
        }
    }
}
