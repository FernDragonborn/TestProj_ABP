﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TestProj_ABP_Backend.DbContext;

#nullable disable

namespace TestProj_ABP_Backend.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20240225225405_AddPriceTestModel")]
    partial class AddPriceTestModel
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TestProj_ABP_Backend.Models.BrowserFingerprint", b =>
                {
                    b.Property<Guid?>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BrowserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BrowserVersion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CollatorInfo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ColorDepth")
                        .HasColumnType("int");

                    b.Property<string>("DefaultLocale")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeviceToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ip")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsCookieEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("Language")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ListFormatInfo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumberFormat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("OnlineStatus")
                        .HasColumnType("bit");

                    b.Property<int?>("Orintation")
                        .HasColumnType("int");

                    b.Property<string>("Os")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float?>("PixelRatio")
                        .HasColumnType("real");

                    b.Property<string>("Plugins")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PluralRulesInfo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RelativeTimeFormatInfo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ScreenHeight")
                        .HasColumnType("int");

                    b.Property<int?>("ScreenWidth")
                        .HasColumnType("int");

                    b.Property<string>("TimeZone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserAgent")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Fingerprints");
                });

            modelBuilder.Entity("TestProj_ABP_Backend.Models.ColorTestModel", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DeviceToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Group")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ColorTest");
                });

            modelBuilder.Entity("TestProj_ABP_Backend.Models.PriceTestModel", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DeviceToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Group")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("PriceTest");
                });

            modelBuilder.Entity("TestProj_ABP_Backend.Models.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeviceToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Version")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TestProj_ABP_Backend.Models.BrowserFingerprint", b =>
                {
                    b.HasOne("TestProj_ABP_Backend.Models.User", "User")
                        .WithOne("BrowserFingerprint")
                        .HasForeignKey("TestProj_ABP_Backend.Models.BrowserFingerprint", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("TestProj_ABP_Backend.Models.ColorTestModel", b =>
                {
                    b.HasOne("TestProj_ABP_Backend.Models.User", "User")
                        .WithOne("ColorExperiment")
                        .HasForeignKey("TestProj_ABP_Backend.Models.ColorTestModel", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("TestProj_ABP_Backend.Models.PriceTestModel", b =>
                {
                    b.HasOne("TestProj_ABP_Backend.Models.User", "User")
                        .WithOne("PriceExperiment")
                        .HasForeignKey("TestProj_ABP_Backend.Models.PriceTestModel", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("TestProj_ABP_Backend.Models.User", b =>
                {
                    b.Navigation("BrowserFingerprint")
                        .IsRequired();

                    b.Navigation("ColorExperiment")
                        .IsRequired();

                    b.Navigation("PriceExperiment")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}