using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace IoDit.WebAPI.Migrations
{
    public partial class no_foreign_key_between_dev_and_devData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeviceData_Devices_DeviceId",
                table: "DeviceData");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Devices",
                table: "Devices");

            migrationBuilder.DropIndex(
                name: "IX_DeviceData_DeviceId",
                table: "DeviceData");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "DeviceId",
                table: "DeviceData");

            migrationBuilder.AddColumn<string>(
                name: "DevEUI",
                table: "DeviceData",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Devices",
                table: "Devices",
                column: "DevEUI");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Devices",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "DevEUI",
                table: "DeviceData");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "Devices",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<long>(
                name: "DeviceId",
                table: "DeviceData",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Devices",
                table: "Devices",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceData_DeviceId",
                table: "DeviceData",
                column: "DeviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceData_Devices_DeviceId",
                table: "DeviceData",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
