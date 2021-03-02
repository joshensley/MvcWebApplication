using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MvcWebApplication.Data.Migrations
{
    public partial class AddItemInformationModelsToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "BoardGameBrand",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BrandName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardGameBrand", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ComicBookBrand",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BrandName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComicBookBrand", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BoardGame",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Controller = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxPlayerNumber = table.Column<int>(type: "int", nullable: false),
                    MinPlayerNumber = table.Column<int>(type: "int", nullable: false),
                    BoardGameBrandID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UPC = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImageFilePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardGame", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BoardGame_BoardGameBrand_BoardGameBrandID",
                        column: x => x.BoardGameBrandID,
                        principalTable: "BoardGameBrand",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComicBook",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Controller = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pages = table.Column<int>(type: "int", nullable: false),
                    ComicBookBrandId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UPC = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImageFilePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComicBook", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ComicBook_ComicBookBrand_ComicBookBrandId",
                        column: x => x.ComicBookBrandId,
                        principalTable: "ComicBookBrand",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryItem",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InStock = table.Column<bool>(type: "bit", nullable: false),
                    ReceivedNumber = table.Column<int>(type: "int", nullable: false),
                    ReceivedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderNumber = table.Column<int>(type: "int", nullable: true),
                    OrderedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ShippingNumber = table.Column<int>(type: "int", nullable: true),
                    ShippedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BoardGameID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComicBookID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_InventoryItem_BoardGame_BoardGameID",
                        column: x => x.BoardGameID,
                        principalTable: "BoardGame",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryItem_ComicBook_ComicBookID",
                        column: x => x.ComicBookID,
                        principalTable: "ComicBook",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoardGame_BoardGameBrandID",
                table: "BoardGame",
                column: "BoardGameBrandID");

            migrationBuilder.CreateIndex(
                name: "IX_ComicBook_ComicBookBrandId",
                table: "ComicBook",
                column: "ComicBookBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItem_BoardGameID",
                table: "InventoryItem",
                column: "BoardGameID");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItem_ComicBookID",
                table: "InventoryItem",
                column: "ComicBookID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InventoryItem");

            migrationBuilder.DropTable(
                name: "BoardGame");

            migrationBuilder.DropTable(
                name: "ComicBook");

            migrationBuilder.DropTable(
                name: "BoardGameBrand");

            migrationBuilder.DropTable(
                name: "ComicBookBrand");

            migrationBuilder.CreateTable(
                name: "BrandBoardGames",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BrandName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrandBoardGames", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BoardGames",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BrandBoardGameID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    ImageFilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlayerNumber = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    UPC = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardGames", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BoardGames_BrandBoardGames_BrandBoardGameID",
                        column: x => x.BrandBoardGameID,
                        principalTable: "BrandBoardGames",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryItemBoardGame",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BoardGameID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InStock = table.Column<bool>(type: "bit", nullable: false),
                    OrderNumber = table.Column<int>(type: "int", nullable: true),
                    OrderedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReceivedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReceivedNumber = table.Column<int>(type: "int", nullable: false),
                    ShippedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ShippingNumber = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryItemBoardGame", x => x.ID);
                    table.ForeignKey(
                        name: "FK_InventoryItemBoardGame_BoardGames_BoardGameID",
                        column: x => x.BoardGameID,
                        principalTable: "BoardGames",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoardGames_BrandBoardGameID",
                table: "BoardGames",
                column: "BrandBoardGameID");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItemBoardGame_BoardGameID",
                table: "InventoryItemBoardGame",
                column: "BoardGameID");
        }
    }
}
