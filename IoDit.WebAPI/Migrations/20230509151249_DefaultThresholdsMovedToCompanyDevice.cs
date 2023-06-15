using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IoDit.WebAPI.Migrations
{
    public partial class DefaultThresholdsMovedToCompanyDevice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultBatteryLevelMax",
                table: "CompanyDeviceData");

            migrationBuilder.DropColumn(
                name: "DefaultBatteryLevelMin",
                table: "CompanyDeviceData");

            migrationBuilder.DropColumn(
                name: "DefaultHumidity1Max",
                table: "CompanyDeviceData");

            migrationBuilder.DropColumn(
                name: "DefaultHumidity1Min",
                table: "CompanyDeviceData");

            migrationBuilder.DropColumn(
                name: "DefaultHumidity2Max",
                table: "CompanyDeviceData");

            migrationBuilder.DropColumn(
                name: "DefaultHumidity2Min",
                table: "CompanyDeviceData");

            migrationBuilder.DropColumn(
                name: "DefaultTemperatureMax",
                table: "CompanyDeviceData");

            migrationBuilder.DropColumn(
                name: "DefaultTemperatureMin",
                table: "CompanyDeviceData");

            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:PostgresExtension:postgis", ",,");

            migrationBuilder.AddColumn<long>(
                name: "DefaultBatteryLevelMax",
                table: "CompanyDevices",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "DefaultBatteryLevelMin",
                table: "CompanyDevices",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "DefaultHumidity1Max",
                table: "CompanyDevices",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "DefaultHumidity1Min",
                table: "CompanyDevices",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "DefaultHumidity2Max",
                table: "CompanyDevices",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "DefaultHumidity2Min",
                table: "CompanyDevices",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "DefaultTemperatureMax",
                table: "CompanyDevices",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "DefaultTemperatureMin",
                table: "CompanyDevices",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultBatteryLevelMax",
                table: "CompanyDevices");

            migrationBuilder.DropColumn(
                name: "DefaultBatteryLevelMin",
                table: "CompanyDevices");

            migrationBuilder.DropColumn(
                name: "DefaultHumidity1Max",
                table: "CompanyDevices");

            migrationBuilder.DropColumn(
                name: "DefaultHumidity1Min",
                table: "CompanyDevices");

            migrationBuilder.DropColumn(
                name: "DefaultHumidity2Max",
                table: "CompanyDevices");

            migrationBuilder.DropColumn(
                name: "DefaultHumidity2Min",
                table: "CompanyDevices");

            migrationBuilder.DropColumn(
                name: "DefaultTemperatureMax",
                table: "CompanyDevices");

            migrationBuilder.DropColumn(
                name: "DefaultTemperatureMin",
                table: "CompanyDevices");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:postgis", ",,");

            migrationBuilder.AddColumn<long>(
                name: "DefaultBatteryLevelMax",
                table: "CompanyDeviceData",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "DefaultBatteryLevelMin",
                table: "CompanyDeviceData",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "DefaultHumidity1Max",
                table: "CompanyDeviceData",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "DefaultHumidity1Min",
                table: "CompanyDeviceData",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "DefaultHumidity2Max",
                table: "CompanyDeviceData",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "DefaultHumidity2Min",
                table: "CompanyDeviceData",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "DefaultTemperatureMax",
                table: "CompanyDeviceData",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "DefaultTemperatureMin",
                table: "CompanyDeviceData",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
