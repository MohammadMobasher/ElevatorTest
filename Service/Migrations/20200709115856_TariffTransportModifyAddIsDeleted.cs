using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Migrations
{
    public partial class TariffTransportModifyAddIsDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TransportationTariff",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TransportationTariff");
        }
    }
}
