using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class AnswerModifyss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FeatureId",
                table: "PackageUserAnswers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FeatureId",
                table: "PackageUserAnswers");
        }
    }
}
