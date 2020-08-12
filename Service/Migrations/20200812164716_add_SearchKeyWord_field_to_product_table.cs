using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class add_SearchKeyWord_field_to_product_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SearchKeyWord",
                table: "Product",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SearchKeyWord",
                table: "Product");
        }
    }
}
