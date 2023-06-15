using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IoDit.WebAPI.Migrations
{
    public partial class AddedFarmToDevice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyFields_Companies_CompanyId",
                table: "CompanyFields");

            migrationBuilder.DropIndex(
                name: "IX_Companies_OwnerId",
                table: "Companies");

            migrationBuilder.AlterColumn<long>(
                name: "CompanyId",
                table: "CompanyFields",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "FarmId",
                table: "CompanyDevices",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDevices_FarmId",
                table: "CompanyDevices",
                column: "FarmId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_OwnerId",
                table: "Companies",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyDevices_CompanyFarms_FarmId",
                table: "CompanyDevices",
                column: "FarmId",
                principalTable: "CompanyFarms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyFields_Companies_CompanyId",
                table: "CompanyFields",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyDevices_CompanyFarms_FarmId",
                table: "CompanyDevices");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyFields_Companies_CompanyId",
                table: "CompanyFields");

            migrationBuilder.DropIndex(
                name: "IX_CompanyDevices_FarmId",
                table: "CompanyDevices");

            migrationBuilder.DropIndex(
                name: "IX_Companies_OwnerId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "FarmId",
                table: "CompanyDevices");

            migrationBuilder.AlterColumn<long>(
                name: "CompanyId",
                table: "CompanyFields",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_OwnerId",
                table: "Companies",
                column: "OwnerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyFields_Companies_CompanyId",
                table: "CompanyFields",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");
        }
    }
}
