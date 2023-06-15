﻿// <auto-generated />
using System;
using IoDit.WebAPI.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetTopologySuite.Geometries;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace IoDit.WebAPI.Migrations
{
    [DbContext(typeof(IoDitDbContext))]
    [Migration("20230504152121_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0-preview.1.23111.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "postgis");
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("IoDit.WebAPI.Persistence.Entities.Company.Company", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("AppId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("AppName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("MaxDevices")
                        .HasColumnType("integer");

                    b.Property<long>("OwnerId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId")
                        .IsUnique();

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("IoDit.WebAPI.Persistence.Entities.Company.CompanyDevice", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("AppKey")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("CompanyId")
                        .HasColumnType("bigint");

                    b.Property<string>("DevEUI")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long?>("FieldId")
                        .HasColumnType("bigint");

                    b.Property<string>("JoinEUI")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("FieldId");

                    b.ToTable("CompanyDevices");
                });

            modelBuilder.Entity("IoDit.WebAPI.Persistence.Entities.Company.CompanyDeviceData", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("BatteryLevel")
                        .HasColumnType("integer");

                    b.Property<long>("DefaultBatteryLevelMax")
                        .HasColumnType("bigint");

                    b.Property<long>("DefaultBatteryLevelMin")
                        .HasColumnType("bigint");

                    b.Property<long>("DefaultHumidity1Max")
                        .HasColumnType("bigint");

                    b.Property<long>("DefaultHumidity1Min")
                        .HasColumnType("bigint");

                    b.Property<long>("DefaultHumidity2Max")
                        .HasColumnType("bigint");

                    b.Property<long>("DefaultHumidity2Min")
                        .HasColumnType("bigint");

                    b.Property<long>("DefaultTemperatureMax")
                        .HasColumnType("bigint");

                    b.Property<long>("DefaultTemperatureMin")
                        .HasColumnType("bigint");

                    b.Property<long>("DeviceId")
                        .HasColumnType("bigint");

                    b.Property<int>("Sensor1")
                        .HasColumnType("integer");

                    b.Property<int>("Sensor2")
                        .HasColumnType("integer");

                    b.Property<int>("Temperature")
                        .HasColumnType("integer");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId");

                    b.ToTable("CompanyDeviceData");
                });

            modelBuilder.Entity("IoDit.WebAPI.Persistence.Entities.Company.CompanyFarm", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("CompanyId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("CompanyFarms");
                });

            modelBuilder.Entity("IoDit.WebAPI.Persistence.Entities.Company.CompanyField", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("CompanyFarmId")
                        .HasColumnType("bigint");

                    b.Property<long?>("CompanyId")
                        .HasColumnType("bigint");

                    b.Property<Geometry>("Geofence")
                        .IsRequired()
                        .HasColumnType("geometry");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CompanyFarmId");

                    b.HasIndex("CompanyId");

                    b.ToTable("CompanyFields");
                });

            modelBuilder.Entity("IoDit.WebAPI.Persistence.Entities.Company.CompanyThresholdPreset", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("CompanyId")
                        .HasColumnType("bigint");

                    b.Property<long>("DefaultBatteryLevelMax")
                        .HasColumnType("bigint");

                    b.Property<long>("DefaultBatteryLevelMin")
                        .HasColumnType("bigint");

                    b.Property<long>("DefaultHumidity1Max")
                        .HasColumnType("bigint");

                    b.Property<long>("DefaultHumidity1Min")
                        .HasColumnType("bigint");

                    b.Property<long>("DefaultHumidity2Max")
                        .HasColumnType("bigint");

                    b.Property<long>("DefaultHumidity2Min")
                        .HasColumnType("bigint");

                    b.Property<long>("DefaultTemperatureMax")
                        .HasColumnType("bigint");

                    b.Property<long>("DefaultTemperatureMin")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("CompanyThresholdPreset");
                });

            modelBuilder.Entity("IoDit.WebAPI.Persistence.Entities.Company.CompanyUser", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("CompanyId")
                        .HasColumnType("bigint");

                    b.Property<int>("CompanyRole")
                        .HasColumnType("integer");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("UserId");

                    b.ToTable("CompanyUsers");
                });

            modelBuilder.Entity("IoDit.WebAPI.Persistence.Entities.Company.CompanyUserData", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("CompanyFarmId")
                        .HasColumnType("bigint");

                    b.Property<int>("CompanyFarmRole")
                        .HasColumnType("integer");

                    b.Property<long>("CompanyId")
                        .HasColumnType("bigint");

                    b.Property<long>("CompanyUserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CompanyFarmId");

                    b.HasIndex("CompanyId");

                    b.HasIndex("CompanyUserId");

                    b.ToTable("CompanyUserData");
                });

            modelBuilder.Entity("IoDit.WebAPI.Persistence.Entities.Company.CompanyUserDeviceData", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("BatteryLevelMax")
                        .HasColumnType("bigint");

                    b.Property<long>("BatteryLevelMin")
                        .HasColumnType("bigint");

                    b.Property<long>("DeviceId")
                        .HasColumnType("bigint");

                    b.Property<long>("Humidity1Max")
                        .HasColumnType("bigint");

                    b.Property<long>("Humidity1Min")
                        .HasColumnType("bigint");

                    b.Property<long>("Humidity2Max")
                        .HasColumnType("bigint");

                    b.Property<long>("Humidity2Min")
                        .HasColumnType("bigint");

                    b.Property<long>("TemperatureMax")
                        .HasColumnType("bigint");

                    b.Property<long>("TemperatureMin")
                        .HasColumnType("bigint");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId");

                    b.HasIndex("UserId");

                    b.ToTable("CompanyUserDeviceData");
                });

            modelBuilder.Entity("IoDit.WebAPI.Persistence.Entities.RefreshToken", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("DeviceIdentifier")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Expires")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("DeviceIdentifier")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("IoDit.WebAPI.Persistence.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("ConfirmationCode")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("ConfirmationExpirationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("ConfirmationTriesCounter")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("IoDit.WebAPI.Persistence.Entities.Company.Company", b =>
                {
                    b.HasOne("IoDit.WebAPI.Persistence.Entities.User", "Owner")
                        .WithOne()
                        .HasForeignKey("IoDit.WebAPI.Persistence.Entities.Company.Company", "OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("IoDit.WebAPI.Persistence.Entities.Company.CompanyDevice", b =>
                {
                    b.HasOne("IoDit.WebAPI.Persistence.Entities.Company.Company", "Company")
                        .WithMany("Devices")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IoDit.WebAPI.Persistence.Entities.Company.CompanyField", "Field")
                        .WithMany("Devices")
                        .HasForeignKey("FieldId");

                    b.Navigation("Company");

                    b.Navigation("Field");
                });

            modelBuilder.Entity("IoDit.WebAPI.Persistence.Entities.Company.CompanyDeviceData", b =>
                {
                    b.HasOne("IoDit.WebAPI.Persistence.Entities.Company.CompanyDevice", "Device")
                        .WithMany("DeviceData")
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");
                });

            modelBuilder.Entity("IoDit.WebAPI.Persistence.Entities.Company.CompanyFarm", b =>
                {
                    b.HasOne("IoDit.WebAPI.Persistence.Entities.Company.Company", "Company")
                        .WithMany("Farms")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("IoDit.WebAPI.Persistence.Entities.Company.CompanyField", b =>
                {
                    b.HasOne("IoDit.WebAPI.Persistence.Entities.Company.CompanyFarm", "CompanyFarm")
                        .WithMany("Fields")
                        .HasForeignKey("CompanyFarmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IoDit.WebAPI.Persistence.Entities.Company.Company", null)
                        .WithMany("Fields")
                        .HasForeignKey("CompanyId");

                    b.Navigation("CompanyFarm");
                });

            modelBuilder.Entity("IoDit.WebAPI.Persistence.Entities.Company.CompanyThresholdPreset", b =>
                {
                    b.HasOne("IoDit.WebAPI.Persistence.Entities.Company.Company", "Company")
                        .WithMany("ThresholdPresets")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("IoDit.WebAPI.Persistence.Entities.Company.CompanyUser", b =>
                {
                    b.HasOne("IoDit.WebAPI.Persistence.Entities.Company.Company", "Company")
                        .WithMany("Users")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IoDit.WebAPI.Persistence.Entities.User", "User")
                        .WithMany("CompanyUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("User");
                });

            modelBuilder.Entity("IoDit.WebAPI.Persistence.Entities.Company.CompanyUserData", b =>
                {
                    b.HasOne("IoDit.WebAPI.Persistence.Entities.Company.CompanyFarm", "CompanyFarm")
                        .WithMany()
                        .HasForeignKey("CompanyFarmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IoDit.WebAPI.Persistence.Entities.Company.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IoDit.WebAPI.Persistence.Entities.Company.CompanyUser", "CompanyUser")
                        .WithMany("CompanyUserData")
                        .HasForeignKey("CompanyUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("CompanyFarm");

                    b.Navigation("CompanyUser");
                });

            modelBuilder.Entity("IoDit.WebAPI.Persistence.Entities.Company.CompanyUserDeviceData", b =>
                {
                    b.HasOne("IoDit.WebAPI.Persistence.Entities.Company.CompanyDevice", "Device")
                        .WithMany()
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IoDit.WebAPI.Persistence.Entities.Company.CompanyUser", "User")
                        .WithMany("CompanyUserDeviceData")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");

                    b.Navigation("User");
                });

            modelBuilder.Entity("IoDit.WebAPI.Persistence.Entities.RefreshToken", b =>
                {
                    b.HasOne("IoDit.WebAPI.Persistence.Entities.User", "User")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("IoDit.WebAPI.Persistence.Entities.Company.Company", b =>
                {
                    b.Navigation("Devices");

                    b.Navigation("Farms");

                    b.Navigation("Fields");

                    b.Navigation("ThresholdPresets");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("IoDit.WebAPI.Persistence.Entities.Company.CompanyDevice", b =>
                {
                    b.Navigation("DeviceData");
                });

            modelBuilder.Entity("IoDit.WebAPI.Persistence.Entities.Company.CompanyFarm", b =>
                {
                    b.Navigation("Fields");
                });

            modelBuilder.Entity("IoDit.WebAPI.Persistence.Entities.Company.CompanyField", b =>
                {
                    b.Navigation("Devices");
                });

            modelBuilder.Entity("IoDit.WebAPI.Persistence.Entities.Company.CompanyUser", b =>
                {
                    b.Navigation("CompanyUserData");

                    b.Navigation("CompanyUserDeviceData");
                });

            modelBuilder.Entity("IoDit.WebAPI.Persistence.Entities.User", b =>
                {
                    b.Navigation("CompanyUsers");

                    b.Navigation("RefreshTokens");
                });
#pragma warning restore 612, 618
        }
    }
}
