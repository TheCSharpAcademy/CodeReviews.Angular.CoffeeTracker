using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kmakai.CoffeeTrackerAPI.Angular.Migrations
{
    /// <inheritdoc />
    public partial class updatedCoffee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "date",
                table: "Coffees",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "cupSize",
                table: "Coffees",
                newName: "CupSize");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Coffees",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Coffees",
                newName: "date");

            migrationBuilder.RenameColumn(
                name: "CupSize",
                table: "Coffees",
                newName: "cupSize");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Coffees",
                newName: "id");
        }
    }
}
