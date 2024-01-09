using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnergyDrinkTracker.Forser_API.Migrations
{
    /// <inheritdoc />
    public partial class RemovedEnergyDrinkModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Records_EnergyDrinks_EnergyDrinkId",
                table: "Records");

            migrationBuilder.DropTable(
                name: "EnergyDrinks");

            migrationBuilder.DropIndex(
                name: "IX_Records_EnergyDrinkId",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "EnergyDrinkId",
                table: "Records");

            migrationBuilder.AddColumn<int>(
                name: "CanSize",
                table: "Records",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "EnergyDrink",
                table: "Records",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanSize",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "EnergyDrink",
                table: "Records");

            migrationBuilder.AddColumn<int>(
                name: "EnergyDrinkId",
                table: "Records",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EnergyDrinks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DrinkName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnergyDrinks", x => x.Id);
                });

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
    }
}
