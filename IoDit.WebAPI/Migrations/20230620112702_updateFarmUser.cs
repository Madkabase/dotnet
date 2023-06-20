using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IoDit.WebAPI.Migrations
{
    public partial class updateFarmUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FarmUser_Farms_FarmId",
                table: "FarmUser");

            migrationBuilder.DropForeignKey(
                name: "FK_FarmUser_Users_UserId",
                table: "FarmUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FarmUser",
                table: "FarmUser");

            migrationBuilder.RenameTable(
                name: "FarmUser",
                newName: "FarmUsers");

            migrationBuilder.RenameIndex(
                name: "IX_FarmUser_UserId",
                table: "FarmUsers",
                newName: "IX_FarmUsers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_FarmUser_FarmId",
                table: "FarmUsers",
                newName: "IX_FarmUsers_FarmId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FarmUsers",
                table: "FarmUsers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FarmUsers_Farms_FarmId",
                table: "FarmUsers",
                column: "FarmId",
                principalTable: "Farms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FarmUsers_Users_UserId",
                table: "FarmUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FarmUsers_Farms_FarmId",
                table: "FarmUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_FarmUsers_Users_UserId",
                table: "FarmUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FarmUsers",
                table: "FarmUsers");

            migrationBuilder.RenameTable(
                name: "FarmUsers",
                newName: "FarmUser");

            migrationBuilder.RenameIndex(
                name: "IX_FarmUsers_UserId",
                table: "FarmUser",
                newName: "IX_FarmUser_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_FarmUsers_FarmId",
                table: "FarmUser",
                newName: "IX_FarmUser_FarmId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FarmUser",
                table: "FarmUser",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FarmUser_Farms_FarmId",
                table: "FarmUser",
                column: "FarmId",
                principalTable: "Farms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FarmUser_Users_UserId",
                table: "FarmUser",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
