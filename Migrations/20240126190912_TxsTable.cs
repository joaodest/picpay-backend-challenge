using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PicpayChallenge.Migrations
{
    /// <inheritdoc />
    public partial class TxsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_CPF_CNPJ",
                table: "Users");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2e649dbc-40d0-401a-aedf-bd3205c1b0cb"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("82d4bce7-379d-4b9f-9728-c37589499182"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ba83bc1c-0dce-4793-b120-aa9d058319b8"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("db24e7b8-27d5-49e9-a9b8-8c8e8d21901e"));

            migrationBuilder.AlterColumn<string>(
                name: "CPF_CNPJ",
                table: "Users",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "CPF_CNPJ",
                table: "Users",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Balance", "CPF_CNPJ", "Email", "Name", "Password", "UserType" },
                values: new object[,]
                {
                    { new Guid("2e649dbc-40d0-401a-aedf-bd3205c1b0cb"), 100.0, "12345678953", "johndoe@example.com", "John Doe", "password123", 0 },
                    { new Guid("82d4bce7-379d-4b9f-9728-c37589499182"), 100.0, "12332678921", "janesmith@example.com", "Jane Smith", "password123", 0 },
                    { new Guid("ba83bc1c-0dce-4793-b120-aa9d058319b8"), 0.0, "56789012334", "normaluser@example.com", "Normal User", "normaluser123", 0 },
                    { new Guid("db24e7b8-27d5-49e9-a9b8-8c8e8d21901e"), 76400.0, "98765432132145", "merchant@example.com", "Merchant", "merchant123", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_CPF_CNPJ",
                table: "Users",
                column: "CPF_CNPJ",
                unique: true);
        }
    }
}
