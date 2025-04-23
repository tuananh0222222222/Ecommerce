using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ecommerce.persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "CreatedAt", "Description", "Name", "Price", "StockQuantity", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("a1b2c3d4-1234-5678-9012-345678901234"), "Điện tử", new DateTime(2025, 4, 23, 15, 23, 11, 154, DateTimeKind.Utc).AddTicks(4855), "Flagship smartphone", "iPhone 15 Pro", 1299.99m, 100, new DateTime(2025, 4, 23, 15, 23, 11, 154, DateTimeKind.Utc).AddTicks(4857) },
                    { new Guid("b2c3d4e5-2345-6789-0123-456789012345"), "Máy tính", new DateTime(2025, 4, 23, 15, 23, 11, 154, DateTimeKind.Utc).AddTicks(4860), "Laptop cao cấp", "MacBook Pro M2", 1999.99m, 50, new DateTime(2025, 4, 23, 15, 23, 11, 154, DateTimeKind.Utc).AddTicks(4860) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-1234-5678-9012-345678901234"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("b2c3d4e5-2345-6789-0123-456789012345"));
        }
    }
}
