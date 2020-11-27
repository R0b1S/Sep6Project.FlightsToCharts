using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlightsToCharts.Data.Migrations
{
    public partial class AddTableMetadata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TablesMetadata",
                columns: table => new
                {
                    Table = table.Column<string>(maxLength: 50, nullable: false),
                    UploadDate = table.Column<DateTime>(nullable: true),
                    EditDate = table.Column<DateTime>(nullable: true),
                    DataMinDate = table.Column<DateTime>(nullable: true),
                    DataMaxDate = table.Column<DateTime>(nullable: true),
                    DataCount = table.Column<double>(nullable: true),
                    DataProcessed = table.Column<bool>(nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TablesMetadata", x => x.Table);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TablesMetadata");
        }
    }
}
