using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PicpayChallenge.Migrations
{
    /// <inheritdoc />
    public partial class AddingAmountToTxTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8aff1f30-cac4-4462-bfea-52e0e5e72e60"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a9ab365c-4f32-4ce0-bf54-a4cca6d792e6"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e01df224-c578-4287-a8fd-60c13f5700fa"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e73496c0-da28-499d-bfca-d7e0d7b9e42b"));

            migrationBuilder.AddColumn<double>(
                name: "Amount",
                table: "Transaction",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Transaction");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Balance", "CPF_CNPJ", "Email", "Name", "Password", "UserType" },
                values: new object[,]
                {
                    { new Guid("8aff1f30-cac4-4462-bfea-52e0e5e72e60"), 100.0, "12345678953", "johndoe@example.com", "John Doe", "password123", 0 },
                    { new Guid("a9ab365c-4f32-4ce0-bf54-a4cca6d792e6"), 100.0, "12332678921", "janesmith@example.com", "Jane Smith", "password123", 0 },
                    { new Guid("e01df224-c578-4287-a8fd-60c13f5700fa"), 76400.0, "98765432132145", "merchant@example.com", "Merchant", "merchant123", 1 },
                    { new Guid("e73496c0-da28-499d-bfca-d7e0d7b9e42b"), 0.0, "56789012334", "normaluser@example.com", "Normal User", "normaluser123", 0 }
                });
        }
    }
}
