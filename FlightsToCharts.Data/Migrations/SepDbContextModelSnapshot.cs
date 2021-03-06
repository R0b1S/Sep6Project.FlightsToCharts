﻿// <auto-generated />
using System;
using FlightsToCharts.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FlightsToCharts.Data.Migrations
{
    [DbContext(typeof(SepDbContext))]
    partial class SepDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("Relational:Sequence:.FlightSeq", "'FlightSeq', '', '1', '1', '', '', 'Int32', 'False'")
                .HasAnnotation("Relational:Sequence:.WeatherSeq", "'WeatherSeq', '', '1', '1', '', '', 'Int32', 'False'")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FlightsToCharts.SharedLibrary.Models.TableMetadata", b =>
                {
                    b.Property<string>("Table")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<double?>("DataCount")
                        .HasColumnType("float");

                    b.Property<DateTime?>("DataMaxDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataMinDate")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("DataProcessed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<DateTime?>("EditDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UploadDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Table");

                    b.ToTable("TablesMetadata");
                });

            modelBuilder.Entity("SharedLibrary.Models.Airline", b =>
                {
                    b.Property<string>("Carrier")
                        .HasColumnType("nvarchar(5)")
                        .HasMaxLength(5);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("Carrier");

                    b.ToTable("Airlines");
                });

            modelBuilder.Entity("SharedLibrary.Models.Airport", b =>
                {
                    b.Property<string>("FAA")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<double?>("Alt")
                        .HasColumnType("float");

                    b.Property<string>("Dst")
                        .HasColumnType("nvarchar(5)")
                        .HasMaxLength(5);

                    b.Property<double?>("Lat")
                        .HasColumnType("float");

                    b.Property<double?>("Lon")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<double?>("Tz")
                        .HasColumnType("float");

                    b.Property<string>("Tzone")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.HasKey("FAA");

                    b.ToTable("Airports");
                });

            modelBuilder.Entity("SharedLibrary.Models.Flight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("NEXT VALUE FOR FlightSeq");

                    b.Property<int?>("AirTime")
                        .HasColumnType("int");

                    b.Property<int?>("ArrDelay")
                        .HasColumnType("int");

                    b.Property<int?>("ArrTime")
                        .HasColumnType("int");

                    b.Property<string>("Carrier")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<int?>("Day")
                        .HasColumnType("int");

                    b.Property<int?>("DepDelay")
                        .HasColumnType("int");

                    b.Property<int?>("DepTime")
                        .HasColumnType("int");

                    b.Property<string>("Dest")
                        .HasColumnType("nvarchar(5)")
                        .HasMaxLength(5);

                    b.Property<int?>("Distance")
                        .HasColumnType("int");

                    b.Property<string>("FlightNo")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<int?>("Hour")
                        .HasColumnType("int");

                    b.Property<int?>("Minute")
                        .HasColumnType("int");

                    b.Property<int?>("Month")
                        .HasColumnType("int");

                    b.Property<string>("Origin")
                        .HasColumnType("nvarchar(5)")
                        .HasMaxLength(5);

                    b.Property<string>("Tailnum")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<int?>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Flights");
                });

            modelBuilder.Entity("SharedLibrary.Models.Plane", b =>
                {
                    b.Property<string>("Tailnum")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("Engine")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<int?>("Engines")
                        .HasColumnType("int");

                    b.Property<string>("Manufacturer")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<int?>("Seats")
                        .HasColumnType("int");

                    b.Property<int?>("Speed")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<int?>("Year")
                        .HasColumnType("int");

                    b.HasKey("Tailnum");

                    b.ToTable("Planes");
                });

            modelBuilder.Entity("SharedLibrary.Models.Weather", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("NEXT VALUE FOR WeatherSeq");

                    b.Property<int?>("Day")
                        .HasColumnType("int");

                    b.Property<double?>("Dewp")
                        .HasColumnType("float");

                    b.Property<int?>("Hour")
                        .HasColumnType("int");

                    b.Property<double?>("Humid")
                        .HasColumnType("float");

                    b.Property<int?>("Month")
                        .HasColumnType("int");

                    b.Property<string>("Origin")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<double?>("Precip")
                        .HasColumnType("float");

                    b.Property<double?>("Pressure")
                        .HasColumnType("float");

                    b.Property<double?>("Temp")
                        .HasColumnType("float");

                    b.Property<DateTime?>("TimeHour")
                        .HasColumnType("datetime2");

                    b.Property<double?>("Visib")
                        .HasColumnType("float");

                    b.Property<int?>("WindDir")
                        .HasColumnType("int");

                    b.Property<double?>("WindGust")
                        .HasColumnType("float");

                    b.Property<double?>("WindSpeed")
                        .HasColumnType("float");

                    b.Property<int?>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Weather");
                });
#pragma warning restore 612, 618
        }
    }
}
