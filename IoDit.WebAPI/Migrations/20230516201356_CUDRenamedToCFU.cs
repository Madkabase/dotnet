using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace IoDit.WebAPI.Migrations
{
    public partial class CUDRenamedToCFU : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyUserData");

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "CompanyUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "CompanyFarmUsers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    CompanyUserId = table.Column<long>(type: "bigint", nullable: false),
                    CompanyFarmId = table.Column<long>(type: "bigint", nullable: false),
                    CompanyFarmRole = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyFarmUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyFarmUsers_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyFarmUsers_CompanyFarms_CompanyFarmId",
                        column: x => x.CompanyFarmId,
                        principalTable: "CompanyFarms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyFarmUsers_CompanyUsers_CompanyUserId",
                        column: x => x.CompanyUserId,
                        principalTable: "CompanyUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyFarmUsers_CompanyFarmId",
                table: "CompanyFarmUsers",
                column: "CompanyFarmId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyFarmUsers_CompanyId",
                table: "CompanyFarmUsers",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyFarmUsers_CompanyUserId",
                table: "CompanyFarmUsers",
                column: "CompanyUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyFarmUsers");

            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "CompanyUsers");

            migrationBuilder.CreateTable(
                name: "CompanyUserData",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CompanyFarmId = table.Column<long>(type: "bigint", nullable: false),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    CompanyUserId = table.Column<long>(type: "bigint", nullable: false),
                    CompanyFarmRole = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyUserData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyUserData_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyUserData_CompanyFarms_CompanyFarmId",
                        column: x => x.CompanyFarmId,
                        principalTable: "CompanyFarms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyUserData_CompanyUsers_CompanyUserId",
                        column: x => x.CompanyUserId,
                        principalTable: "CompanyUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyUserData_CompanyFarmId",
                table: "CompanyUserData",
                column: "CompanyFarmId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyUserData_CompanyId",
                table: "CompanyUserData",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyUserData_CompanyUserId",
                table: "CompanyUserData",
                column: "CompanyUserId");
        }
    }
}
