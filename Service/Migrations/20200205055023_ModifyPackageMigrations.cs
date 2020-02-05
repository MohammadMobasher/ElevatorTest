using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class ModifyPackageMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSpecialPackage",
                table: "ProductPackage",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSpecialPackage",
                table: "ProductPackage");
        }
    }
}
