using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class IntialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FoodAndDrinkOption",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LactoseFree = table.Column<bool>(type: "bit", nullable: false),
                    NutFree = table.Column<bool>(type: "bit", nullable: false),
                    Vegetarian = table.Column<bool>(type: "bit", nullable: false),
                    NonAlcoholic = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodAndDrinkOption", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BoardGameNights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrganizerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateAndTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    MaxPlayers = table.Column<int>(type: "int", nullable: false),
                    FoodAndDrinkOptionsId = table.Column<int>(type: "int", nullable: true),
                    SelectedBoardGameId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardGameNights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BoardGameNights_FoodAndDrinkOption_FoodAndDrinkOptionsId",
                        column: x => x.FoodAndDrinkOptionsId,
                        principalTable: "FoodAndDrinkOption",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BoardGames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Genre = table.Column<int>(type: "int", nullable: false),
                    Is18Plus = table.Column<bool>(type: "bit", nullable: false),
                    PhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GameType_Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BoardGameNightId = table.Column<int>(type: "int", nullable: true),
                    BoardGameNightId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardGames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BoardGames_BoardGameNights_BoardGameNightId",
                        column: x => x.BoardGameNightId,
                        principalTable: "BoardGameNights",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BoardGames_BoardGameNights_BoardGameNightId1",
                        column: x => x.BoardGameNightId1,
                        principalTable: "BoardGameNights",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BoardGameNightId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Player_BoardGameNights_BoardGameNightId",
                        column: x => x.BoardGameNightId,
                        principalTable: "BoardGameNights",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoardGameNights_FoodAndDrinkOptionsId",
                table: "BoardGameNights",
                column: "FoodAndDrinkOptionsId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardGames_BoardGameNightId",
                table: "BoardGames",
                column: "BoardGameNightId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardGames_BoardGameNightId1",
                table: "BoardGames",
                column: "BoardGameNightId1");

            migrationBuilder.CreateIndex(
                name: "IX_Player_BoardGameNightId",
                table: "Player",
                column: "BoardGameNightId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoardGames");

            migrationBuilder.DropTable(
                name: "Player");

            migrationBuilder.DropTable(
                name: "BoardGameNights");

            migrationBuilder.DropTable(
                name: "FoodAndDrinkOption");
        }
    }
}
