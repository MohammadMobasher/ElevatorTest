using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class recreateProductGroupFeature : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "GroupFeature");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "GroupFeature");

            migrationBuilder.AddColumn<int>(
                name: "FeatureId",
                table: "GroupFeature",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_GroupFeature_FeatureId",
                table: "GroupFeature",
                column: "FeatureId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupFeature_Feature_FeatureId",
                table: "GroupFeature",
                column: "FeatureId",
                principalTable: "Feature",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupFeature_Feature_FeatureId",
                table: "GroupFeature");

            migrationBuilder.DropIndex(
                name: "IX_GroupFeature_FeatureId",
                table: "GroupFeature");

            migrationBuilder.DropColumn(
                name: "FeatureId",
                table: "GroupFeature");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "GroupFeature",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "GroupFeature",
                maxLength: 150,
                nullable: false,
                defaultValue: "");
        }
    }
}
