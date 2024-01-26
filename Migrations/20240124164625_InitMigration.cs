using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PicpayChallenge.Migrations
{
    /// <inheritdoc />
    public partial class InitMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    Email = table.Column<string>(type: "longtext", nullable: false),
                    Password = table.Column<string>(type: "longtext", nullable: false),
                    Balance = table.Column<double>(type: "double", nullable: false),
                    CPF_CNPJ = table.Column<string>(type: "varchar(255)", nullable: false),
                    UserType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FromUserId = table.Column<Guid>(type: "char(36)", nullable: false),
                    ToUserId = table.Column<Guid>(type: "char(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaction_Users_FromUserId",
                        column: x => x.FromUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transaction_Users_ToUserId",
                        column: x => x.ToUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

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
                name: "IX_Transaction_FromUserId",
                table: "Transaction",
                column: "FromUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_ToUserId",
                table: "Transaction",
                column: "ToUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CPF_CNPJ",
                table: "Users",
                column: "CPF_CNPJ",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
