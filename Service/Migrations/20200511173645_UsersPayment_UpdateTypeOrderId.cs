using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class UsersPayment_UpdateTypeOrderId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OrderId",
                table: "UsersPayment",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "UsersPayment",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
