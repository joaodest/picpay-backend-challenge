using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PicpayChallenge.Migrations
{
    /// <inheritdoc />
    public partial class SplittingTxsHist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Users_FromUserId",
                table: "Transaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Users_ToUserId",
                table: "Transaction");

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
                    { new Guid("36add61c-d461-4e86-89c0-97d515ce8588"), 76400.0, "98765432132145", "merchant@example.com", "Merchant", "merchant123", 1 },
                    { new Guid("53b7061d-f683-4980-878a-be7071120bdd"), 0.0, "56789012334", "normaluser@example.com", "Normal User", "normaluser123", 0 },
                    { new Guid("90bb0649-e8ab-464a-902b-765dcf6fead3"), 100.0, "12332678921", "janesmith@example.com", "Jane Smith", "password123", 0 },
                    { new Guid("f5dd1930-4579-42e3-87b2-98af8242631f"), 100.0, "12345678953", "johndoe@example.com", "John Doe", "password123", 0 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Users_FromUserId",
                table: "Transaction",
                column: "FromUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Users_ToUserId",
                table: "Transaction",
                column: "ToUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Users_FromUserId",
                table: "Transaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Users_ToUserId",
                table: "Transaction");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("36add61c-d461-4e86-89c0-97d515ce8588"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("53b7061d-f683-4980-878a-be7071120bdd"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("90bb0649-e8ab-464a-902b-765dcf6fead3"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f5dd1930-4579-42e3-87b2-98af8242631f"));

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

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Users_FromUserId",
                table: "Transaction",
                column: "FromUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Users_ToUserId",
                table: "Transaction",
                column: "ToUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
