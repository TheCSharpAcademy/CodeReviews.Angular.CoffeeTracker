using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kmakai.CoffeeTrackerAPI.Angular.Migrations
{
    /// <inheritdoc />
    public partial class updatedCoffee2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CupSize",
                table: "Coffees",
                newName: "Cups");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Cups",
                table: "Coffees",
                newName: "CupSize");
        }
    }
}
