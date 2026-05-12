using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CafeTracker.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialDBCafeRecords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CafeRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    Category = table.Column<int>(type: "integer", nullable: false),
                    DateConsumed = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CafeRecords", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "CafeRecords",
                columns: new[] { "Id", "Category", "DateConsumed", "DateCreated", "DateModified", "Notes", "ProductName", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTimeOffset(new DateTime(2026, 5, 2, 15, 45, 20, 391, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 5, 2, 15, 45, 20, 391, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Had with breakfast", "Cappuccino", 5 },
                    { 2, 1, new DateTime(2026, 5, 6, 11, 30, 19, 672, DateTimeKind.Utc), new DateTimeOffset(new DateTime(2026, 5, 4, 11, 29, 3, 739, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 5, 4, 11, 29, 3, 739, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Served with breakfast", "Cappuccino", 2 },
                    { 3, 1, new DateTime(2026, 5, 2, 0, 0, 0, 0, DateTimeKind.Utc), new DateTimeOffset(new DateTime(2026, 5, 4, 11, 29, 18, 170, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 5, 4, 11, 29, 18, 170, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Had with dinner", "Latte", 3 },
                    { 4, 1, new DateTime(2026, 5, 3, 0, 0, 0, 0, DateTimeKind.Utc), new DateTimeOffset(new DateTime(2026, 5, 4, 11, 29, 30, 65, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 5, 4, 11, 29, 30, 65, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Early morning meeting prep", "Americano", 1 },
                    { 5, 1, new DateTime(2026, 5, 4, 0, 0, 0, 0, DateTimeKind.Utc), new DateTimeOffset(new DateTime(2026, 5, 4, 11, 29, 40, 838, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 5, 4, 11, 29, 40, 838, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Coffee break with friends", "Mocha", 2 },
                    { 6, 1, new DateTime(2026, 5, 5, 0, 0, 0, 0, DateTimeKind.Utc), new DateTimeOffset(new DateTime(2026, 5, 4, 11, 29, 55, 56, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 5, 4, 11, 29, 55, 56, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Needed energy for coding session", "Flat White", 2 },
                    { 7, 1, new DateTime(2026, 5, 5, 0, 0, 0, 0, DateTimeKind.Utc), new DateTimeOffset(new DateTime(2026, 5, 4, 11, 30, 4, 288, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 5, 4, 11, 30, 4, 288, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Short coffee break", "Macchiato", 1 },
                    { 8, 4, new DateTime(2026, 5, 6, 0, 0, 0, 0, DateTimeKind.Utc), new DateTimeOffset(new DateTime(2026, 5, 4, 11, 30, 13, 238, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 5, 4, 11, 30, 13, 238, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Very hot afternoon", "Cold Brew", 3 },
                    { 9, 1, new DateTime(2026, 5, 6, 0, 0, 0, 0, DateTimeKind.Utc), new DateTimeOffset(new DateTime(2026, 5, 4, 11, 30, 21, 227, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 5, 4, 11, 30, 21, 227, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Relaxing evening drink", "Irish Coffee", 1 },
                    { 10, 1, new DateTime(2026, 5, 7, 0, 0, 0, 0, DateTimeKind.Utc), new DateTimeOffset(new DateTime(2026, 5, 4, 11, 30, 28, 810, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 5, 4, 11, 30, 28, 810, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Breakfast companion", "Vanilla Latte", 2 },
                    { 11, 1, new DateTime(2010, 10, 10, 0, 0, 0, 0, DateTimeKind.Utc), new DateTimeOffset(new DateTime(2026, 5, 6, 11, 45, 8, 57, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 5, 6, 11, 45, 8, 57, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Delicious and healthy", "Espresso", 10 },
                    { 13, 8, new DateTime(2010, 10, 10, 0, 0, 0, 0, DateTimeKind.Utc), new DateTimeOffset(new DateTime(2026, 5, 9, 14, 12, 43, 178, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 5, 9, 14, 12, 43, 178, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Cold, creamy and tasty", "Hollandia Yoghurt", 2 },
                    { 15, 3, new DateTime(2001, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTimeOffset(new DateTime(2026, 5, 9, 14, 24, 10, 550, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2026, 5, 9, 14, 24, 10, 550, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Cold beverage for everyday nourishment", "Hot Choco", 10 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CafeRecords_ProductName",
                table: "CafeRecords",
                column: "ProductName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CafeRecords");
        }
    }
}
