using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IoDit.WebAPI.Migrations
{
    public partial class renameHumidityToMoisture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Humidity2Min",
                table: "Thresholds",
                newName: "Moisture2Min");

            migrationBuilder.RenameColumn(
                name: "Humidity2Max",
                table: "Thresholds",
                newName: "Moisture2Max");

            migrationBuilder.RenameColumn(
                name: "Humidity1Min",
                table: "Thresholds",
                newName: "Moisture1Min");

            migrationBuilder.RenameColumn(
                name: "Humidity1Max",
                table: "Thresholds",
                newName: "Moisture1Max");

            migrationBuilder.RenameColumn(
                name: "Humidity2Min",
                table: "ThresholdPresets",
                newName: "Moisture2Min");

            migrationBuilder.RenameColumn(
                name: "Humidity2Max",
                table: "ThresholdPresets",
                newName: "Moisture2Max");

            migrationBuilder.RenameColumn(
                name: "Humidity1Min",
                table: "ThresholdPresets",
                newName: "Moisture1Min");

            migrationBuilder.RenameColumn(
                name: "Humidity1Max",
                table: "ThresholdPresets",
                newName: "Moisture1Max");

            migrationBuilder.RenameColumn(
                name: "Humidity2Min",
                table: "GlobalThresholdPresets",
                newName: "Moisture2Min");

            migrationBuilder.RenameColumn(
                name: "Humidity2Max",
                table: "GlobalThresholdPresets",
                newName: "Moisture2Max");

            migrationBuilder.RenameColumn(
                name: "Humidity1Min",
                table: "GlobalThresholdPresets",
                newName: "Moisture1Min");

            migrationBuilder.RenameColumn(
                name: "Humidity1Max",
                table: "GlobalThresholdPresets",
                newName: "Moisture1Max");

            migrationBuilder.RenameColumn(
                name: "Humidity2",
                table: "DeviceData",
                newName: "Moisture2");

            migrationBuilder.RenameColumn(
                name: "Humidity1",
                table: "DeviceData",
                newName: "Moisture1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Moisture2Min",
                table: "Thresholds",
                newName: "Humidity2Min");

            migrationBuilder.RenameColumn(
                name: "Moisture2Max",
                table: "Thresholds",
                newName: "Humidity2Max");

            migrationBuilder.RenameColumn(
                name: "Moisture1Min",
                table: "Thresholds",
                newName: "Humidity1Min");

            migrationBuilder.RenameColumn(
                name: "Moisture1Max",
                table: "Thresholds",
                newName: "Humidity1Max");

            migrationBuilder.RenameColumn(
                name: "Moisture2Min",
                table: "ThresholdPresets",
                newName: "Humidity2Min");

            migrationBuilder.RenameColumn(
                name: "Moisture2Max",
                table: "ThresholdPresets",
                newName: "Humidity2Max");

            migrationBuilder.RenameColumn(
                name: "Moisture1Min",
                table: "ThresholdPresets",
                newName: "Humidity1Min");

            migrationBuilder.RenameColumn(
                name: "Moisture1Max",
                table: "ThresholdPresets",
                newName: "Humidity1Max");

            migrationBuilder.RenameColumn(
                name: "Moisture2Min",
                table: "GlobalThresholdPresets",
                newName: "Humidity2Min");

            migrationBuilder.RenameColumn(
                name: "Moisture2Max",
                table: "GlobalThresholdPresets",
                newName: "Humidity2Max");

            migrationBuilder.RenameColumn(
                name: "Moisture1Min",
                table: "GlobalThresholdPresets",
                newName: "Humidity1Min");

            migrationBuilder.RenameColumn(
                name: "Moisture1Max",
                table: "GlobalThresholdPresets",
                newName: "Humidity1Max");

            migrationBuilder.RenameColumn(
                name: "Moisture2",
                table: "DeviceData",
                newName: "Humidity2");

            migrationBuilder.RenameColumn(
                name: "Moisture1",
                table: "DeviceData",
                newName: "Humidity1");
        }
    }
}
