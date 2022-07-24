using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mango.Services.ProductAPI.Migrations
{
    public partial class SeedProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryName", "Description", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Stsrter", "fried pastry with a savory filling of spiced potatoes, onions, and peas", "https://robgit28.blob.core.windows.net/mango/samosas.jpeg", "Samosas", 3.0 },
                    { 2, "Stsrter", "Crispy Onion Bhajis with thinly sliced onions, mixed and coated in an chickpea flour batter", "https://robgit28.blob.core.windows.net/mango/onionBhajis.jpeg", "Onion Bhajis", 3.5 },
                    { 3, "Stsrter", "Paneer pakoras fried, cheesy squares, served with green & red chili chutney", "https://robgit28.blob.core.windows.net/mango/paneerPakora.jpeg", "Paneer Pakora", 2.5 },
                    { 4, "Stsrter", "Potato Cauliflower Vegetable Curry", "https://robgit28.blob.core.windows.net/mango/alooGobi.jpeg", "Aloo Gobi", 4.5 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4);
        }
    }
}
