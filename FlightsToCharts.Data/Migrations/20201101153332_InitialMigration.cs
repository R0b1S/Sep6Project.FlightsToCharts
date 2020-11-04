using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlightsToCharts.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "FlightSeq");

            migrationBuilder.CreateSequence<int>(
                name: "WeatherSeq");

            migrationBuilder.CreateTable(
                name: "Airlines",
                columns: table => new
                {
                    Carrier = table.Column<string>(maxLength: 5, nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airlines", x => x.Carrier);
                });

            migrationBuilder.CreateTable(
                name: "Airports",
                columns: table => new
                {
                    FAA = table.Column<string>(maxLength: 10, nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: true),
                    Lon = table.Column<double>(nullable: true),
                    Lat = table.Column<double>(nullable: true),
                    Alt = table.Column<double>(nullable: true),
                    Tz = table.Column<double>(nullable: true),
                    Dst = table.Column<string>(maxLength: 5, nullable: true),
                    Tzone = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airports", x => x.FAA);
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValueSql: "NEXT VALUE FOR FlightSeq"),
                    Year = table.Column<int>(nullable: true),
                    Month = table.Column<int>(nullable: true),
                    Day = table.Column<int>(nullable: true),
                    DepTime = table.Column<int>(nullable: true),
                    ArrTime = table.Column<int>(nullable: true),
                    DepDelay = table.Column<int>(nullable: true),
                    ArrDelay = table.Column<int>(nullable: true),
                    Hour = table.Column<int>(nullable: true),
                    Minute = table.Column<int>(nullable: true),
                    Carrier = table.Column<string>(maxLength: 200, nullable: true),
                    Tailnum = table.Column<string>(maxLength: 20, nullable: true),
                    FlightNo = table.Column<string>(maxLength: 20, nullable: true),
                    Origin = table.Column<string>(maxLength: 5, nullable: true),
                    Dest = table.Column<string>(maxLength: 5, nullable: true),
                    AirTime = table.Column<int>(nullable: false),
                    Distance = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Planes",
                columns: table => new
                {
                    Tailnum = table.Column<string>(maxLength: 20, nullable: false),
                    Year = table.Column<int>(nullable: true),
                    Type = table.Column<string>(maxLength: 100, nullable: true),
                    Manufacturer = table.Column<string>(maxLength: 100, nullable: true),
                    Model = table.Column<string>(maxLength: 100, nullable: true),
                    Engines = table.Column<int>(nullable: true),
                    Seats = table.Column<int>(nullable: true),
                    Speed = table.Column<int>(nullable: true),
                    Engine = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planes", x => x.Tailnum);
                });

            migrationBuilder.CreateTable(
                name: "Weather",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValueSql: "NEXT VALUE FOR WeatherSeq"),
                    Origin = table.Column<string>(maxLength: 10, nullable: true),
                    Year = table.Column<int>(nullable: true),
                    Month = table.Column<int>(nullable: true),
                    Day = table.Column<int>(nullable: true),
                    Hour = table.Column<int>(nullable: true),
                    Temp = table.Column<double>(nullable: true),
                    Dewp = table.Column<double>(nullable: true),
                    Humid = table.Column<double>(nullable: true),
                    WindDir = table.Column<int>(nullable: true),
                    WindSpeed = table.Column<double>(nullable: true),
                    WindGust = table.Column<double>(nullable: true),
                    Precip = table.Column<double>(nullable: true),
                    Pressure = table.Column<double>(nullable: true),
                    Visib = table.Column<double>(nullable: true),
                    TimeHour = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weather", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Airlines");

            migrationBuilder.DropTable(
                name: "Airports");

            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "Planes");

            migrationBuilder.DropTable(
                name: "Weather");

            migrationBuilder.DropSequence(
                name: "FlightSeq");

            migrationBuilder.DropSequence(
                name: "WeatherSeq");
        }
    }
}
