using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PicpayChallenge.Migrations
{
    /// <inheritdoc />
    public partial class TransactionMovedToDomain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transaction");

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

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FromUserId = table.Column<Guid>(type: "char(36)", nullable: false),
                    ToUserId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Amount = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Users_FromUserId",
                        column: x => x.FromUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transactions_Users_ToUserId",
                        column: x => x.ToUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

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

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_FromUserId",
                table: "Transactions",
                column: "FromUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ToUserId",
                table: "Transactions",
                column: "ToUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

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

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    FromUserId = table.Column<Guid>(type: "char(36)", nullable: false),
                    ToUserId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Amount = table.Column<double>(type: "double", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaction_Users_FromUserId",
                        column: x => x.FromUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transaction_Users_ToUserId",
                        column: x => x.ToUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

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

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_FromUserId",
                table: "Transaction",
                column: "FromUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_ToUserId",
                table: "Transaction",
                column: "ToUserId");
        }
    }
}
