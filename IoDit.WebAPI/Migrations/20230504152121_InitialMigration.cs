using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace IoDit.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:postgis", ",,");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    IsVerified = table.Column<bool>(type: "boolean", nullable: false),
                    ConfirmationCode = table.Column<long>(type: "bigint", nullable: false),
                    ConfirmationExpirationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConfirmationTriesCounter = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OwnerId = table.Column<long>(type: "bigint", nullable: false),
                    CompanyName = table.Column<string>(type: "text", nullable: false),
                    AppId = table.Column<string>(type: "text", nullable: false),
                    AppName = table.Column<string>(type: "text", nullable: false),
                    MaxDevices = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companies_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Token = table.Column<string>(type: "text", nullable: false),
                    Expires = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeviceIdentifier = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyFarms",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyFarms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyFarms_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyThresholdPreset",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DefaultHumidity1Min = table.Column<long>(type: "bigint", nullable: false),
                    DefaultHumidity1Max = table.Column<long>(type: "bigint", nullable: false),
                    DefaultHumidity2Min = table.Column<long>(type: "bigint", nullable: false),
                    DefaultHumidity2Max = table.Column<long>(type: "bigint", nullable: false),
                    DefaultBatteryLevelMin = table.Column<long>(type: "bigint", nullable: false),
                    DefaultBatteryLevelMax = table.Column<long>(type: "bigint", nullable: false),
                    DefaultTemperatureMin = table.Column<long>(type: "bigint", nullable: false),
                    DefaultTemperatureMax = table.Column<long>(type: "bigint", nullable: false),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyThresholdPreset", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyThresholdPreset_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyUsers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    CompanyRole = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyUsers_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyFields",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CompanyFarmId = table.Column<long>(type: "bigint", nullable: false),
                    Geofence = table.Column<Geometry>(type: "geometry", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CompanyId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyFields_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompanyFields_CompanyFarms_CompanyFarmId",
                        column: x => x.CompanyFarmId,
                        principalTable: "CompanyFarms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyUserData",
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

            migrationBuilder.CreateTable(
                name: "CompanyDevices",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DevEUI = table.Column<string>(type: "text", nullable: false),
                    JoinEUI = table.Column<string>(type: "text", nullable: false),
                    AppKey = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false),
                    FieldId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyDevices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyDevices_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyDevices_CompanyFields_FieldId",
                        column: x => x.FieldId,
                        principalTable: "CompanyFields",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompanyDeviceData",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DeviceId = table.Column<long>(type: "bigint", nullable: false),
                    Sensor1 = table.Column<int>(type: "integer", nullable: false),
                    Sensor2 = table.Column<int>(type: "integer", nullable: false),
                    BatteryLevel = table.Column<int>(type: "integer", nullable: false),
                    Temperature = table.Column<int>(type: "integer", nullable: false),
                    DefaultHumidity1Min = table.Column<long>(type: "bigint", nullable: false),
                    DefaultHumidity1Max = table.Column<long>(type: "bigint", nullable: false),
                    DefaultHumidity2Min = table.Column<long>(type: "bigint", nullable: false),
                    DefaultHumidity2Max = table.Column<long>(type: "bigint", nullable: false),
                    DefaultBatteryLevelMin = table.Column<long>(type: "bigint", nullable: false),
                    DefaultBatteryLevelMax = table.Column<long>(type: "bigint", nullable: false),
                    DefaultTemperatureMin = table.Column<long>(type: "bigint", nullable: false),
                    DefaultTemperatureMax = table.Column<long>(type: "bigint", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyDeviceData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyDeviceData_CompanyDevices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "CompanyDevices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyUserDeviceData",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DeviceId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Humidity1Min = table.Column<long>(type: "bigint", nullable: false),
                    Humidity1Max = table.Column<long>(type: "bigint", nullable: false),
                    Humidity2Min = table.Column<long>(type: "bigint", nullable: false),
                    Humidity2Max = table.Column<long>(type: "bigint", nullable: false),
                    BatteryLevelMin = table.Column<long>(type: "bigint", nullable: false),
                    BatteryLevelMax = table.Column<long>(type: "bigint", nullable: false),
                    TemperatureMin = table.Column<long>(type: "bigint", nullable: false),
                    TemperatureMax = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyUserDeviceData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyUserDeviceData_CompanyDevices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "CompanyDevices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyUserDeviceData_CompanyUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "CompanyUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_OwnerId",
                table: "Companies",
                column: "OwnerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDeviceData_DeviceId",
                table: "CompanyDeviceData",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDevices_CompanyId",
                table: "CompanyDevices",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDevices_FieldId",
                table: "CompanyDevices",
                column: "FieldId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyFarms_CompanyId",
                table: "CompanyFarms",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyFields_CompanyFarmId",
                table: "CompanyFields",
                column: "CompanyFarmId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyFields_CompanyId",
                table: "CompanyFields",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyThresholdPreset_CompanyId",
                table: "CompanyThresholdPreset",
                column: "CompanyId");

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

            migrationBuilder.CreateIndex(
                name: "IX_CompanyUserDeviceData_DeviceId",
                table: "CompanyUserDeviceData",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyUserDeviceData_UserId",
                table: "CompanyUserDeviceData",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyUsers_CompanyId",
                table: "CompanyUsers",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyUsers_UserId",
                table: "CompanyUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_DeviceIdentifier",
                table: "RefreshTokens",
                column: "DeviceIdentifier",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyDeviceData");

            migrationBuilder.DropTable(
                name: "CompanyThresholdPreset");

            migrationBuilder.DropTable(
                name: "CompanyUserData");

            migrationBuilder.DropTable(
                name: "CompanyUserDeviceData");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "CompanyDevices");

            migrationBuilder.DropTable(
                name: "CompanyUsers");

            migrationBuilder.DropTable(
                name: "CompanyFields");

            migrationBuilder.DropTable(
                name: "CompanyFarms");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
