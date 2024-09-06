using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CoffeeTracker.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CoffeeSet",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConsumptionDate = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoffeeSet", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "CoffeeSet",
                columns: new[] { "Id", "Avatar", "ConsumptionDate", "Name" },
                values: new object[,]
                {
                    { "01bf0667-5885-4959-8152-d46b2fee0496", "cappuccino_k.jpg", "3.09.2024", "Cappuccino" },
                    { "1044ce4a-a125-4981-bd65-cce00e00a350", "americano_k.jpg", "6.09.2024", "Americano" },
                    { "18b20fb9-ddad-424b-bc98-08b931876614", "latte_k.jpg", "3.09.2024", "Latte" },
                    { "271da484-ea54-4347-85a9-3d617831ad12", "mocha_k.jpg", "4.09.2024", "Mocha" },
                    { "2e61dc4b-b0ae-4c88-8b0c-f817e7e43b2f", "mocha_k.jpg", "5.09.2024", "Mocha" },
                    { "30e050a1-2f8a-4329-afd9-8abb1b08820e", "latte_k.jpg", "4.09.2024", "Latte" },
                    { "32247123-5690-48ce-b40c-81f59eabfd8a", "cappuccino_k.jpg", "6.09.2024", "Cappuccino" },
                    { "384107e7-5712-479c-aac5-eea187699920", "latte_k.jpg", "1.09.2024", "Latte" },
                    { "3a27050d-1c94-4525-b93c-b83c23ad4c7c", "mocha_k.jpg", "3.09.2024", "Mocha" },
                    { "4ba0f9fc-ad6a-4ffe-9a82-c40e5b9e4056", "filtered_k.jpg", "6.09.2024", "Filtered Coffee" },
                    { "6555dac3-7c66-48fc-b0f5-b72657692bfe", "americano_k.jpg", "31.08.2024", "Americano" },
                    { "66533037-57a0-469e-ba10-98871946fe5a", "cappuccino_k.jpg", "6.09.2024", "Cappuccino" },
                    { "a8259409-9f87-4baa-9f8d-55cb66a8cd41", "cappuccino_k.jpg", "2.09.2024", "Cappuccino" },
                    { "b07cd535-5a14-4d88-9c53-abd1c0fbb192", "filtered_k.jpg", "1.09.2024", "Filtered Coffee" },
                    { "cdfe82af-b3cf-4a53-ba40-1c4ca6ff2582", "turkish_k.jpg", "5.09.2024", "Turkish Coffee" },
                    { "d0ebfa3a-56a7-4239-86b4-b887481fa640", "turkish_k.jpg", "31.08.2024", "Turkish Coffee" },
                    { "d50f8b16-308f-4382-afdc-8482bf79a97c", "filtered_k.jpg", "2.09.2024", "Filtered Coffee" },
                    { "e033bec8-c5a1-4113-b89a-d8f2e7256309", "turkish_k.jpg", "5.09.2024", "Turkish Coffee" },
                    { "ea7a362e-a31b-4b06-b223-901b63672ee6", "cappuccino_k.jpg", "4.09.2024", "Cappuccino" },
                    { "fb1b5441-3de7-4508-af87-9ae06be140a2", "cappuccino_k.jpg", "4.09.2024", "Cappuccino" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoffeeSet");
        }
    }
}
