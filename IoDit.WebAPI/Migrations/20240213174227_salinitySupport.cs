using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IoDit.WebAPI.Migrations
{
    public partial class salinitySupport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Temperature2Min",
                table: "Thresholds",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "Temperature2Max",
                table: "Thresholds",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "Temperature1Min",
                table: "Thresholds",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "Temperature1Max",
                table: "Thresholds",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AddColumn<float>(
                name: "Salinity1Max",
                table: "Thresholds",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Salinity1Min",
                table: "Thresholds",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Salinity2Max",
                table: "Thresholds",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Salinity2Min",
                table: "Thresholds",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AlterColumn<float>(
                name: "Temperature2Min",
                table: "ThresholdPresets",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "Temperature2Max",
                table: "ThresholdPresets",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "Temperature1Min",
                table: "ThresholdPresets",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "Temperature1Max",
                table: "ThresholdPresets",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "Temperature2Min",
                table: "GlobalThresholdPresets",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "Temperature2Max",
                table: "GlobalThresholdPresets",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "Temperature1Min",
                table: "GlobalThresholdPresets",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<float>(
                name: "Temperature1Max",
                table: "GlobalThresholdPresets",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AddColumn<float>(
                name: "Salinity1Max",
                table: "GlobalThresholdPresets",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Salinity1Min",
                table: "GlobalThresholdPresets",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Salinity2Max",
                table: "GlobalThresholdPresets",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Salinity2Min",
                table: "GlobalThresholdPresets",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Salinity1",
                table: "DeviceData",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Salinity2",
                table: "DeviceData",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Salinity1Max",
                table: "Thresholds");

            migrationBuilder.DropColumn(
                name: "Salinity1Min",
                table: "Thresholds");

            migrationBuilder.DropColumn(
                name: "Salinity2Max",
                table: "Thresholds");

            migrationBuilder.DropColumn(
                name: "Salinity2Min",
                table: "Thresholds");

            migrationBuilder.DropColumn(
                name: "Salinity1Max",
                table: "GlobalThresholdPresets");

            migrationBuilder.DropColumn(
                name: "Salinity1Min",
                table: "GlobalThresholdPresets");

            migrationBuilder.DropColumn(
                name: "Salinity2Max",
                table: "GlobalThresholdPresets");

            migrationBuilder.DropColumn(
                name: "Salinity2Min",
                table: "GlobalThresholdPresets");

            migrationBuilder.DropColumn(
                name: "Salinity1",
                table: "DeviceData");

            migrationBuilder.DropColumn(
                name: "Salinity2",
                table: "DeviceData");

            migrationBuilder.AlterColumn<double>(
                name: "Temperature2Min",
                table: "Thresholds",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "Temperature2Max",
                table: "Thresholds",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "Temperature1Min",
                table: "Thresholds",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "Temperature1Max",
                table: "Thresholds",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "Temperature2Min",
                table: "ThresholdPresets",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "Temperature2Max",
                table: "ThresholdPresets",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "Temperature1Min",
                table: "ThresholdPresets",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "Temperature1Max",
                table: "ThresholdPresets",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "Temperature2Min",
                table: "GlobalThresholdPresets",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "Temperature2Max",
                table: "GlobalThresholdPresets",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "Temperature1Min",
                table: "GlobalThresholdPresets",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "Temperature1Max",
                table: "GlobalThresholdPresets",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }
    }
}
