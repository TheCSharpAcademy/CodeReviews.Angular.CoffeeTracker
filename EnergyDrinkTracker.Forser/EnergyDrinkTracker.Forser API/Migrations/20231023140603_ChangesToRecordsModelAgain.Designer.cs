﻿// <auto-generated />
using System;
using EnergyDrinkTracker.Forser_API.DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EnergyDrinkTracker.Forser_API.Migrations
{
    [DbContext(typeof(DrinkDbContext))]
    [Migration("20231023140603_ChangesToRecordsModelAgain")]
    partial class ChangesToRecordsModelAgain
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EnergyDrinkTracker.Forser_API.Models.EnergyDrink", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DrinkName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Size")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("EnergyDrinks");
                });

            modelBuilder.Entity("EnergyDrinkTracker.Forser_API.Models.Records", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DrinkDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("EnergyDrinkId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EnergyDrinkId");

                    b.ToTable("Records");
                });

            modelBuilder.Entity("EnergyDrinkTracker.Forser_API.Models.Records", b =>
                {
                    b.HasOne("EnergyDrinkTracker.Forser_API.Models.EnergyDrink", "EnergyDrink")
                        .WithMany()
                        .HasForeignKey("EnergyDrinkId");

                    b.Navigation("EnergyDrink");
                });
#pragma warning restore 612, 618
        }
    }
}
