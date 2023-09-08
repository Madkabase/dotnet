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
    [DbContext(typeof(AgroditDbContext))]
    [Migration("20230620112702_updateFarmUser")]
    partial class updateFarmUser
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("IoDit.WebAPI.Persistence.Entities.Device", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("AppKey")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("DevEUI")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("FieldId")
                        .HasColumnType("bigint");

                    b.Property<string>("JoinEUI")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("FieldId");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("IoDit.WebAPI.Persistence.Entities.DeviceData", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("BatteryLevel")
                        .HasColumnType("integer");

                    b.Property<long>("DeviceId")
                        .HasColumnType("bigint");

                    b.Property<int>("Humidity1")
                        .HasColumnType("integer");

                    b.Property<int>("Humidity2")
                        .HasColumnType("integer");

                    b.Property<float>("Temperature")
                        .HasColumnType("real");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId");

                    b.ToTable("DeviceData");
                });

            modelBuilder.Entity("IoDit.WebAPI.Persistence.Entities.Farm", b =>
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

                    b.Property<int>("MaxDevices")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("OwnerId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Farms");
                });

            modelBuilder.Entity("IoDit.WebAPI.Persistence.Entities.FarmUser", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("FarmId")
                        .HasColumnType("bigint");

                    b.Property<int>("FarmRole")
                        .HasColumnType("integer");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("FarmId");

                    b.HasIndex("UserId");

                    b.ToTable("FarmUsers");
                });

            modelBuilder.Entity("IoDit.WebAPI.Persistence.Entities.Field", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("FarmId")
                        .HasColumnType("bigint");

                    b.Property<Geometry>("Geofence")
                        .IsRequired()
                        .HasColumnType("geometry");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("ThresholdId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("FarmId");

                    b.HasIndex("ThresholdId")
                        .IsUnique();

                    b.ToTable("Fields");
                });

            modelBuilder.Entity("IoDit.WebAPI.Persistence.Entities.GlobalThresholdPreset", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("BatteryLevelMax")
                        .HasColumnType("integer");

                    b.Property<int>("BatteryLevelMin")
                        .HasColumnType("integer");

                    b.Property<int>("Humidity1Max")
                        .HasColumnType("integer");

                    b.Property<int>("Humidity1Min")
                        .HasColumnType("integer");

                    b.Property<int>("Humidity2Max")
                        .HasColumnType("integer");

                    b.Property<int>("Humidity2Min")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("TemperatureMax")
                        .HasColumnType("double precision");

                    b.Property<double>("TemperatureMin")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("GlobalThresholdPresets");
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

                    b.HasIndex("UserId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("IoDit.WebAPI.Persistence.Entities.SubscriptionRequest", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("FarmName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsFulfilled")
                        .HasColumnType("boolean");

                    b.Property<int>("MaxDevices")
                        .HasColumnType("integer");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("SubscriptionRequests");
                });

            modelBuilder.Entity("IoDit.WebAPI.Persistence.Entities.Threshold", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("BatteryLevelMax")
                        .HasColumnType("integer");

                    b.Property<int>("BatteryLevelMin")
                        .HasColumnType("integer");

                    b.Property<int>("Humidity1Max")
                        .HasColumnType("integer");

                    b.Property<int>("Humidity1Min")
                        .HasColumnType("integer");

                    b.Property<int>("Humidity2Max")
                        .HasColumnType("integer");

                    b.Property<int>("Humidity2Min")
                        .HasColumnType("integer");

                    b.Property<double>("TemperatureMax")
                        .HasColumnType("double precision");

                    b.Property<double>("TemperatureMin")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("Thresholds");
                });

            modelBuilder.Entity("IoDit.WebAPI.Persistence.Entities.ThresholdPreset", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("BatteryLevelMax")
                        .HasColumnType("integer");

                    b.Property<int>("BatteryLevelMin")
                        .HasColumnType("integer");

                    b.Property<long>("FarmId")
                        .HasColumnType("bigint");

                    b.Property<int>("Humidity1Max")
                        .HasColumnType("integer");

                    b.Property<int>("Humidity1Min")
                        .HasColumnType("integer");

                    b.Property<int>("Humidity2Max")
                        .HasColumnType("integer");

                    b.Property<int>("Humidity2Min")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("TemperatureMax")
                        .HasColumnType("double precision");

                    b.Property<double>("TemperatureMin")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("FarmId");

                    b.ToTable("ThresholdPresets");
                });

            modelBuilder.Entity("IoDit.WebAPI.Persistence.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("AppRole")
                        .HasColumnType("integer");

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

                    b.ToTable("Users");
                });

            modelBuilder.Entity("IoDit.WebAPI.Persistence.Entities.Device", b =>
                {
                    b.HasOne("IoDit.WebAPI.Persistence.Entities.Field", "Field")
                        .WithMany("Devices")
                        .HasForeignKey("FieldId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Field");
                });

            modelBuilder.Entity("IoDit.WebAPI.Persistence.Entities.DeviceData", b =>
                {
                    b.HasOne("IoDit.WebAPI.Persistence.Entities.Device", "Device")
                        .WithMany("DeviceData")
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");
                });

            modelBuilder.Entity("IoDit.WebAPI.Persistence.Entities.Farm", b =>
                {
                    b.HasOne("IoDit.WebAPI.Persistence.Entities.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("IoDit.WebAPI.Persistence.Entities.FarmUser", b =>
                {
                    b.HasOne("IoDit.WebAPI.Persistence.Entities.Farm", "Farm")
                        .WithMany("FarmUsers")
                        .HasForeignKey("FarmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IoDit.WebAPI.Persistence.Entities.User", "User")
                        .WithMany("FarmUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Farm");

                    b.Navigation("User");
                });

            modelBuilder.Entity("IoDit.WebAPI.Persistence.Entities.Field", b =>
                {
                    b.HasOne("IoDit.WebAPI.Persistence.Entities.Farm", "Farm")
                        .WithMany("Fields")
                        .HasForeignKey("FarmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IoDit.WebAPI.Persistence.Entities.Threshold", "Threshold")
                        .WithOne("Field")
                        .HasForeignKey("IoDit.WebAPI.Persistence.Entities.Field", "ThresholdId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Farm");

                    b.Navigation("Threshold");
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

            modelBuilder.Entity("IoDit.WebAPI.Persistence.Entities.SubscriptionRequest", b =>
                {
                    b.HasOne("IoDit.WebAPI.Persistence.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("IoDit.WebAPI.Persistence.Entities.ThresholdPreset", b =>
                {
                    b.HasOne("IoDit.WebAPI.Persistence.Entities.Farm", "Farm")
                        .WithMany("ThresholdPresets")
                        .HasForeignKey("FarmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Farm");
                });

            modelBuilder.Entity("IoDit.WebAPI.Persistence.Entities.Device", b =>
                {
                    b.Navigation("DeviceData");
                });

            modelBuilder.Entity("IoDit.WebAPI.Persistence.Entities.Farm", b =>
                {
                    b.Navigation("FarmUsers");

                    b.Navigation("Fields");

                    b.Navigation("ThresholdPresets");
                });

            modelBuilder.Entity("IoDit.WebAPI.Persistence.Entities.Field", b =>
                {
                    b.Navigation("Devices");
                });

            modelBuilder.Entity("IoDit.WebAPI.Persistence.Entities.Threshold", b =>
                {
                    b.Navigation("Field")
                        .IsRequired();
                });

            modelBuilder.Entity("IoDit.WebAPI.Persistence.Entities.User", b =>
                {
                    b.Navigation("FarmUsers");

                    b.Navigation("RefreshTokens");
                });
#pragma warning restore 612, 618
        }
    }
}
