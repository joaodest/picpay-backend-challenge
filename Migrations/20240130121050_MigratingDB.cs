using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PicpayChallenge.Migrations
{
    /// <inheritdoc />
    public partial class MigratingDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("260625f2-4afd-47c8-943d-698bfd7ab410"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5989e242-4c27-4ec5-abf2-b2f76378b904"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("914b6023-3c48-4ef6-98f6-75b5a815e6fd"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("cea5646a-a641-4b08-88cb-c9ba51a06db7"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Balance", "CPF_CNPJ", "Email", "Name", "Password", "UserType" },
                values: new object[,]
                {
                    { new Guid("260625f2-4afd-47c8-943d-698bfd7ab410"), 0.0, "56789012334", "normaluser@example.com", "Normal User", "normaluser123", 0 },
                    { new Guid("5989e242-4c27-4ec5-abf2-b2f76378b904"), 100.0, "12345678953", "johndoe@example.com", "John Doe", "password123", 0 },
                    { new Guid("914b6023-3c48-4ef6-98f6-75b5a815e6fd"), 76400.0, "98765432132145", "merchant@example.com", "Merchant", "merchant123", 1 },
                    { new Guid("cea5646a-a641-4b08-88cb-c9ba51a06db7"), 100.0, "12332678921", "janesmith@example.com", "Jane Smith", "password123", 0 }
                });
        }
    }
}
