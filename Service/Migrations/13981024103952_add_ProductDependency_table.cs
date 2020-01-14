using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class add_ProductDependency_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dependency");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Product",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.CreateTable(
                name: "ProductDependencies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 300, nullable: false),
                    GroupId1 = table.Column<int>(nullable: false),
                    Feature1 = table.Column<int>(nullable: false),
                    Value1 = table.Column<string>(nullable: true),
                    GroupId2 = table.Column<int>(nullable: false),
                    Feature2 = table.Column<int>(nullable: false),
                    Value2 = table.Column<string>(nullable: true),
                    ConditionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDependencies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductDependencies_Condition_ConditionId",
                        column: x => x.ConditionId,
                        principalTable: "Condition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductDependencies_ProductGroup_GroupId1",
                        column: x => x.GroupId1,
                        principalTable: "ProductGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductDependencies_ConditionId",
                table: "ProductDependencies",
                column: "ConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDependencies_GroupId1",
                table: "ProductDependencies",
                column: "GroupId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductDependencies");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Product",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Dependency",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ConditionId = table.Column<int>(nullable: false),
                    Feature1 = table.Column<int>(nullable: false),
                    Feature2 = table.Column<int>(nullable: false),
                    GroupId1 = table.Column<int>(nullable: false),
                    GroupId2 = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    Value1 = table.Column<string>(nullable: true),
                    Value2 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dependency", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dependency_Condition_ConditionId",
                        column: x => x.ConditionId,
                        principalTable: "Condition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dependency_ConditionId",
                table: "Dependency",
                column: "ConditionId");
        }
    }
}
