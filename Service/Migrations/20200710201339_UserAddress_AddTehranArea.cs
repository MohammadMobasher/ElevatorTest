using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class UserAddress_AddTehranArea : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TehranAreasFrom",
                table: "UserAddress",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TehranAreasFrom",
                table: "UserAddress");
        }
    }
}
