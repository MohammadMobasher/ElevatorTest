using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class remove_FeatureId_relation_in_PackageUserAnswer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_PackageUserAnswers_Feature_FeatureId",
            //    table: "PackageUserAnswers");

            //migrationBuilder.DropIndex(
            //    name: "IX_PackageUserAnswers_FeatureId",
            //    table: "PackageUserAnswers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateIndex(
            //    name: "IX_PackageUserAnswers_FeatureId",
            //    table: "PackageUserAnswers",
            //    column: "FeatureId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_PackageUserAnswers_Feature_FeatureId",
            //    table: "PackageUserAnswers",
            //    column: "FeatureId",
            //    principalTable: "Feature",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }
    }
}
