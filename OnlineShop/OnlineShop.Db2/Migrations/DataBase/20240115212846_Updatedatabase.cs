using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineShop.Db.Migrations.Database
{
    /// <inheritdoc />
    public partial class Updatedatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "37f1db03-1823-4740-80b1-57066840073e");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "72fa08e5-25a5-424f-be24-15b9c66a6caf");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "f60ac4b9-f4d0-42b8-bc4d-bdceaeaba0e4");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Bonus", "Cost", "Description", "ImgPath", "Name" },
                values: new object[,]
                {
                    { "04eb2a60-bc04-4892-9045-58c595ff3320", 55, 760m, "С сыром пармезан и помидорами черри", "/Images/Products/margarita.jpg", "Маргарита" },
                    { "2b64ca5c-01c4-4413-8f14-58c0b36f87f5", 55, 770m, "С шампиньонами с картошкой", "/Images/Products/gribnaya.jpg", "Грибная" },
                    { "f966e96c-6a73-46c8-99ad-f75a16fbf4ba", 60, 780m, "С сыром пармезан и листьями салата", "/Images/Products/cezar.jpg", "Цезарь" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "04eb2a60-bc04-4892-9045-58c595ff3320");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "2b64ca5c-01c4-4413-8f14-58c0b36f87f5");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: "f966e96c-6a73-46c8-99ad-f75a16fbf4ba");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Bonus", "Cost", "Description", "ImgPath", "Name" },
                values: new object[,]
                {
                    { "37f1db03-1823-4740-80b1-57066840073e", 55, 760m, "С сыром пармезан и помидорами черри", "/Images/Products/margarita.jpg", "Маргарита" },
                    { "72fa08e5-25a5-424f-be24-15b9c66a6caf", 60, 780m, "С сыром пармезан и листьями салата", "/Images/Products/cezar.jpg", "Цезарь" },
                    { "f60ac4b9-f4d0-42b8-bc4d-bdceaeaba0e4", 55, 770m, "С шампиньонами с картошкой", "/Images/Products/gribnaya.jpg", "Грибная" }
                });
        }
    }
}
