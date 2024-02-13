using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IoDit.WebAPI.Migrations
{
    public partial class TemperatureLow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BatteryLevelMax",
                table: "Thresholds");

            migrationBuilder.DropColumn(
                name: "BatteryLevelMin",
                table: "Thresholds");

            migrationBuilder.DropColumn(
                name: "BatteryLevelMax",
                table: "ThresholdPresets");

            migrationBuilder.DropColumn(
                name: "BatteryLevelMin",
                table: "ThresholdPresets");

            migrationBuilder.DropColumn(
                name: "BatteryLevelMax",
                table: "GlobalThresholdPresets");

            migrationBuilder.DropColumn(
                name: "BatteryLevelMin",
                table: "GlobalThresholdPresets");

            migrationBuilder.RenameColumn(
                name: "TemperatureMin",
                table: "Thresholds",
                newName: "Temperature1Min");

            migrationBuilder.RenameColumn(
                name: "TemperatureMax",
                table: "Thresholds",
                newName: "Temperature1Max");

            migrationBuilder.RenameColumn(
                name: "TemperatureMin",
                table: "ThresholdPresets",
                newName: "Temperature1Min");

            migrationBuilder.RenameColumn(
                name: "TemperatureMax",
                table: "ThresholdPresets",
                newName: "Temperature1Max");

            migrationBuilder.RenameColumn(
                name: "TemperatureMin",
                table: "GlobalThresholdPresets",
                newName: "Temperature1Min");

            migrationBuilder.RenameColumn(
                name: "TemperatureMax",
                table: "GlobalThresholdPresets",
                newName: "Temperature1Max");

            migrationBuilder.RenameColumn(
                name: "Temperature",
                table: "DeviceData",
                newName: "Temperature1");

            migrationBuilder.AddColumn<double>(
                name: "Temperature2Max",
                table: "Thresholds",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Temperature2Min",
                table: "Thresholds",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Temperature2Max",
                table: "ThresholdPresets",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Temperature2Min",
                table: "ThresholdPresets",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Temperature2Max",
                table: "GlobalThresholdPresets",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Temperature2Min",
                table: "GlobalThresholdPresets",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<float>(
                name: "Temperature2",
                table: "DeviceData",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Temperature2Max",
                table: "Thresholds");

            migrationBuilder.DropColumn(
                name: "Temperature2Min",
                table: "Thresholds");

            migrationBuilder.DropColumn(
                name: "Temperature2Max",
                table: "ThresholdPresets");

            migrationBuilder.DropColumn(
                name: "Temperature2Min",
                table: "ThresholdPresets");

            migrationBuilder.DropColumn(
                name: "Temperature2Max",
                table: "GlobalThresholdPresets");

            migrationBuilder.DropColumn(
                name: "Temperature2Min",
                table: "GlobalThresholdPresets");

            migrationBuilder.DropColumn(
                name: "Temperature2",
                table: "DeviceData");

            migrationBuilder.RenameColumn(
                name: "Temperatur12Min",
                table: "Thresholds",
                newName: "TemperatureMin");

            migrationBuilder.RenameColumn(
                name: "Temperature1Max",
                table: "Thresholds",
                newName: "TemperatureMax");

            migrationBuilder.RenameColumn(
                name: "Temperature1Min",
                table: "ThresholdPresets",
                newName: "TemperatureMin");

            migrationBuilder.RenameColumn(
                name: "Temperature1Max",
                table: "ThresholdPresets",
                newName: "TemperatureMax");

            migrationBuilder.RenameColumn(
                name: "Temperature1Min",
                table: "GlobalThresholdPresets",
                newName: "TemperatureMin");

            migrationBuilder.RenameColumn(
                name: "Temperature1Max",
                table: "GlobalThresholdPresets",
                newName: "TemperatureMax");

            migrationBuilder.RenameColumn(
                name: "Temperature1",
                table: "DeviceData",
                newName: "Temperature");

            migrationBuilder.AddColumn<int>(
                name: "BatteryLevelMax",
                table: "Thresholds",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BatteryLevelMin",
                table: "Thresholds",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BatteryLevelMax",
                table: "ThresholdPresets",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BatteryLevelMin",
                table: "ThresholdPresets",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BatteryLevelMax",
                table: "GlobalThresholdPresets",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BatteryLevelMin",
                table: "GlobalThresholdPresets",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
