using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class OutOfTehranUserAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOutTehran",
                table: "UserAddress",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Province",
                table: "UserAddress",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOutTehran",
                table: "UserAddress");

            migrationBuilder.DropColumn(
                name: "Province",
                table: "UserAddress");
        }
    }
}
