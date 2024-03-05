using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IoDit.WebAPI.Migrations
{
    public partial class calibrationDataForSensor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CalibrationMoisture1Max",
                table: "Devices",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CalibrationMoisture1Min",
                table: "Devices",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CalibrationMoisture2Max",
                table: "Devices",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CalibrationMoisture2Min",
                table: "Devices",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CalibrationSalinity1Max",
                table: "Devices",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CalibrationSalinity1Min",
                table: "Devices",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CalibrationSalinity2Max",
                table: "Devices",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CalibrationSalinity2Min",
                table: "Devices",
                type: "integer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CalibrationMoisture1Max",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "CalibrationMoisture1Min",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "CalibrationMoisture2Max",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "CalibrationMoisture2Min",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "CalibrationSalinity1Max",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "CalibrationSalinity1Min",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "CalibrationSalinity2Max",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "CalibrationSalinity2Min",
                table: "Devices");
        }
    }
}
