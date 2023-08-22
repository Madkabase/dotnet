using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IoDit.WebAPI.Migrations
{
    public partial class threshold_field_id_nullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Fields_FieldId",
                table: "Devices");

            migrationBuilder.DropForeignKey(
                name: "FK_Fields_Thresholds_ThresholdId",
                table: "Fields");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Fields_FieldId",
                table: "Devices",
                column: "FieldId",
                principalTable: "Fields",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Fields_Thresholds_ThresholdId",
                table: "Fields",
                column: "ThresholdId",
                principalTable: "Thresholds",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Fields_FieldId",
                table: "Devices");

            migrationBuilder.DropForeignKey(
                name: "FK_Fields_Thresholds_ThresholdId",
                table: "Fields");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Fields_FieldId",
                table: "Devices",
                column: "FieldId",
                principalTable: "Fields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Fields_Thresholds_ThresholdId",
                table: "Fields",
                column: "ThresholdId",
                principalTable: "Thresholds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
