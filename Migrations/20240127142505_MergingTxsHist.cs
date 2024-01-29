using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PicpayChallenge.Migrations
{
    /// <inheritdoc />
    public partial class MergingTxsHist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("15bda43e-1aaf-4cbb-b236-2cd53582fdab"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("7f9d0ee1-3908-478a-9735-26f908c1af37"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d824c720-3ffa-4e82-a461-3bc136202d5b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e27fed9d-3943-4075-a35e-4a26874bf0c5"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Balance", "CPF_CNPJ", "Email", "Name", "Password", "UserType" },
                values: new object[,]
                {
                    { new Guid("044a5fb0-608d-4009-9c2e-29d59422c5b6"), 100.0, "12332678921", "janesmith@example.com", "Jane Smith", "password123", 0 },
                    { new Guid("18ef7ea5-2b37-44c0-8a08-ce18548f85e6"), 76400.0, "98765432132145", "merchant@example.com", "Merchant", "merchant123", 1 },
                    { new Guid("5aff7013-4a16-47bc-bdca-8ef0bd692846"), 100.0, "12345678953", "johndoe@example.com", "John Doe", "password123", 0 },
                    { new Guid("fe0008f7-d268-48bf-af7a-d75193229a45"), 0.0, "56789012334", "normaluser@example.com", "Normal User", "normaluser123", 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("044a5fb0-608d-4009-9c2e-29d59422c5b6"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("18ef7ea5-2b37-44c0-8a08-ce18548f85e6"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5aff7013-4a16-47bc-bdca-8ef0bd692846"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("fe0008f7-d268-48bf-af7a-d75193229a45"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Balance", "CPF_CNPJ", "Email", "Name", "Password", "UserType" },
                values: new object[,]
                {
                    { new Guid("15bda43e-1aaf-4cbb-b236-2cd53582fdab"), 76400.0, "98765432132145", "merchant@example.com", "Merchant", "merchant123", 1 },
                    { new Guid("7f9d0ee1-3908-478a-9735-26f908c1af37"), 100.0, "12345678953", "johndoe@example.com", "John Doe", "password123", 0 },
                    { new Guid("d824c720-3ffa-4e82-a461-3bc136202d5b"), 100.0, "12332678921", "janesmith@example.com", "Jane Smith", "password123", 0 },
                    { new Guid("e27fed9d-3943-4075-a35e-4a26874bf0c5"), 0.0, "56789012334", "normaluser@example.com", "Normal User", "normaluser123", 0 }
                });
        }
    }
}
