using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnergyDrinkTracker.Forser_API.Migrations
{
    /// <inheritdoc />
    public partial class ChangesToRecordsModelAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDranked",
                table: "Records");

            migrationBuilder.CreateIndex(
                name: "IX_Records_EnergyDrinkId",
                table: "Records",
                column: "EnergyDrinkId");

            migrationBuilder.AddForeignKey(
                name: "FK_Records_EnergyDrinks_EnergyDrinkId",
                table: "Records",
                column: "EnergyDrinkId",
                principalTable: "EnergyDrinks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Records_EnergyDrinks_EnergyDrinkId",
                table: "Records");

            migrationBuilder.DropIndex(
                name: "IX_Records_EnergyDrinkId",
                table: "Records");

            migrationBuilder.AddColumn<bool>(
                name: "IsDranked",
                table: "Records",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
