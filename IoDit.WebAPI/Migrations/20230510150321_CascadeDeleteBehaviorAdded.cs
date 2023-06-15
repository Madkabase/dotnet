using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IoDit.WebAPI.Migrations
{
    public partial class CascadeDeleteBehaviorAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyDevices_CompanyFields_FieldId",
                table: "CompanyDevices");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyDevices_CompanyFields_FieldId",
                table: "CompanyDevices",
                column: "FieldId",
                principalTable: "CompanyFields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyDevices_CompanyFields_FieldId",
                table: "CompanyDevices");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyDevices_CompanyFields_FieldId",
                table: "CompanyDevices",
                column: "FieldId",
                principalTable: "CompanyFields",
                principalColumn: "Id");
        }
    }
}
