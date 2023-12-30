using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeTracker.UgniusFalze.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CoffeeRecords",
                columns: table => new
                {
                    CoffeeRecordId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CupsOfCoffee = table.Column<int>(type: "int", nullable: false),
                    RecordDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoffeeRecords", x => x.CoffeeRecordId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoffeeRecords");
        }
    }
}
