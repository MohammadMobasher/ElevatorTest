using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class PackageUserAnswer_ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Answer",
                table: "PackageUserAnswers",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Answer",
                table: "PackageUserAnswers",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
