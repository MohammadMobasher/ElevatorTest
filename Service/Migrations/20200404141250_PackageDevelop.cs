using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class PackageDevelop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsQuestion",
                table: "Feature",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "QuestionTitle",
                table: "Feature",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PackageUserAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FeatureId = table.Column<int>(nullable: false),
                    Answer = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    IsManager = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageUserAnswers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PackageUserAnswers");

            migrationBuilder.DropColumn(
                name: "IsQuestion",
                table: "Feature");

            migrationBuilder.DropColumn(
                name: "QuestionTitle",
                table: "Feature");
        }
    }
}
