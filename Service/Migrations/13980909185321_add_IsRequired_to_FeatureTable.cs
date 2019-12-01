using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class add_IsRequired_to_FeatureTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SSOTValue",
                table: "Feature");

            migrationBuilder.AddColumn<bool>(
                name: "IsRequired",
                table: "Feature",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRequired",
                table: "Feature");

            migrationBuilder.AddColumn<string>(
                name: "SSOTValue",
                table: "Feature",
                nullable: true);
        }
    }
}
