using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnergyDrinkTracker.Forser_API.Migrations
{
    /// <inheritdoc />
    public partial class ChangesToRecordsModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnergyDrinks_Records_RecordsId",
                table: "EnergyDrinks");

            migrationBuilder.DropIndex(
                name: "IX_EnergyDrinks_RecordsId",
                table: "EnergyDrinks");

            migrationBuilder.DropColumn(
                name: "RecordsId",
                table: "EnergyDrinks");

            migrationBuilder.AddColumn<int>(
                name: "EnergyDrinkId",
                table: "Records",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnergyDrinkId",
                table: "Records");

            migrationBuilder.AddColumn<int>(
                name: "RecordsId",
                table: "EnergyDrinks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EnergyDrinks_RecordsId",
                table: "EnergyDrinks",
                column: "RecordsId");

            migrationBuilder.AddForeignKey(
                name: "FK_EnergyDrinks_Records_RecordsId",
                table: "EnergyDrinks",
                column: "RecordsId",
                principalTable: "Records",
                principalColumn: "Id");
        }
    }
}
