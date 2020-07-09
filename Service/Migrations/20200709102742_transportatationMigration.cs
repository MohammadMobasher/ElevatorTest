using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class transportatationMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarTransport",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CarName = table.Column<string>(nullable: true),
                    CarModel = table.Column<string>(nullable: true),
                    Plaque = table.Column<string>(nullable: true),
                    MotorSerial = table.Column<string>(nullable: true),
                    TransportSize = table.Column<int>(nullable: false),
                    Pic = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarTransport", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransportationTariff",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TehranAreasFrom = table.Column<int>(nullable: false),
                    TehranAreasTO = table.Column<int>(nullable: false),
                    CarId = table.Column<int>(nullable: false),
                    ProductSize = table.Column<int>(nullable: false),
                    Tariff = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportationTariff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransportationTariff_CarTransport_CarId",
                        column: x => x.CarId,
                        principalTable: "CarTransport",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransportationTariff_CarId",
                table: "TransportationTariff",
                column: "CarId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransportationTariff");

            migrationBuilder.DropTable(
                name: "CarTransport");
        }
    }
}
