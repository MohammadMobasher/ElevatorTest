using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class TariffTransportModify : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransportationTariff_CarTransport_CarId",
                table: "TransportationTariff");

            migrationBuilder.AlterColumn<int>(
                name: "CarId",
                table: "TransportationTariff",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_TransportationTariff_CarTransport_CarId",
                table: "TransportationTariff",
                column: "CarId",
                principalTable: "CarTransport",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransportationTariff_CarTransport_CarId",
                table: "TransportationTariff");

            migrationBuilder.AlterColumn<int>(
                name: "CarId",
                table: "TransportationTariff",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TransportationTariff_CarTransport_CarId",
                table: "TransportationTariff",
                column: "CarId",
                principalTable: "CarTransport",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
