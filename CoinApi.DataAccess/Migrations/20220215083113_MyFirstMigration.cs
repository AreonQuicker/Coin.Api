using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoinApi.DataAccess.Migrations
{
    public partial class MyFirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rank = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coin", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CoinFavorite",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Symbol = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoinFavorite", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CryptoCurrency",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Symbol = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CmcRank = table.Column<decimal>(type: "decimal(29,10)", nullable: false),
                    NumMarketPairs = table.Column<decimal>(type: "decimal(29,10)", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CryptoCurrency", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currency",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Sign = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CurrencyFavorite",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Symbol = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CoinFavoriteId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyFavorite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CurrencyFavorite_CoinFavorite_CoinFavoriteId",
                        column: x => x.CoinFavoriteId,
                        principalTable: "CoinFavorite",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CryptoCurrencyQuote",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Symbol = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(29,10)", nullable: false),
                    Volume24H = table.Column<decimal>(type: "decimal(29,10)", nullable: false),
                    MarketCap = table.Column<decimal>(type: "decimal(29,10)", nullable: false),
                    CryptoCurrencyId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CryptoCurrencyQuote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CryptoCurrencyQuote_CryptoCurrency_CryptoCurrencyId",
                        column: x => x.CryptoCurrencyId,
                        principalTable: "CryptoCurrency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CryptoCurrencyQuote_CryptoCurrencyId",
                table: "CryptoCurrencyQuote",
                column: "CryptoCurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Currency_Sign_Symbol",
                table: "Currency",
                columns: new[] { "Sign", "Symbol" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyFavorite_CoinFavoriteId_Symbol",
                table: "CurrencyFavorite",
                columns: new[] { "CoinFavoriteId", "Symbol" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coin");

            migrationBuilder.DropTable(
                name: "CryptoCurrencyQuote");

            migrationBuilder.DropTable(
                name: "Currency");

            migrationBuilder.DropTable(
                name: "CurrencyFavorite");

            migrationBuilder.DropTable(
                name: "CryptoCurrency");

            migrationBuilder.DropTable(
                name: "CoinFavorite");
        }
    }
}
