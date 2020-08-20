using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class add_DiscountComputed_Product : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discount",
                table: "Product",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "Product");
        }
    }
}
