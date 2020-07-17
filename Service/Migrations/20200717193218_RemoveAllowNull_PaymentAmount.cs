using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class RemoveAllowNull_PaymentAmount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "PaymentAmount",
                table: "ShopOrder",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "PaymentAmount",
                table: "ShopOrder",
                nullable: true,
                oldClrType: typeof(long));
        }
    }
}
