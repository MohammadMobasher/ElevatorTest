using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class SgopOders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFactorSubmited",
                table: "Product");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFactorSubmited",
                table: "Product",
                nullable: false,
                defaultValue: false);
        }
    }
}
