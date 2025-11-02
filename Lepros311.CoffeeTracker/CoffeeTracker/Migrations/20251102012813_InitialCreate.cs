using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CoffeeTracker.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coffees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coffees", x => x.Id);
                    table.CheckConstraint("CK_Coffees_Price", "Price > 0");
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateAndTimeOfSale = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CoffeeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoffeeId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sales_Coffees_CoffeeId",
                        column: x => x.CoffeeId,
                        principalTable: "Coffees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Coffees",
                columns: new[] { "Id", "IsDeleted", "Name", "Price" },
                values: new object[,]
                {
                    { 1, false, "Black Coffee", 1.62m },
                    { 2, false, "Espresso", 4.42m },
                    { 3, false, "Mocha", 5.39m },
                    { 4, false, "Latte", 3.99m },
                    { 5, false, "Cappuccino", 7.15m },
                    { 6, false, "Mazagran", 6.47m },
                    { 7, false, "Breve", 6.33m },
                    { 8, false, "Macchiato", 5.87m },
                    { 9, false, "Cortado", 8.29m },
                    { 10, false, "Dirty Chai", 4.29m },
                    { 11, false, "Irish Coffee", 5.80m },
                    { 12, false, "Turkish Coffee", 9.15m }
                });

            migrationBuilder.InsertData(
                table: "Sales",
                columns: new[] { "Id", "CoffeeId", "CoffeeName", "DateAndTimeOfSale", "IsDeleted", "Total" },
                values: new object[] { 1, 1, "Black Coffee", new DateTime(2023, 10, 9, 10, 30, 0, 0, DateTimeKind.Unspecified), false, 1.62m });

            migrationBuilder.CreateIndex(
                name: "IX_Sales_CoffeeId",
                table: "Sales",
                column: "CoffeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "Coffees");
        }
    }
}
