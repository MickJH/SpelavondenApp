using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePlayersModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoardGames_BoardGameNights_BoardGameNightId1",
                table: "BoardGames");

            migrationBuilder.DropIndex(
                name: "IX_BoardGames_BoardGameNightId1",
                table: "BoardGames");

            migrationBuilder.DropColumn(
                name: "BoardGameNightId1",
                table: "BoardGames");

            migrationBuilder.AddColumn<DateTime>(
                name: "JoinDateTime",
                table: "Player",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_BoardGameNights_SelectedBoardGameId",
                table: "BoardGameNights",
                column: "SelectedBoardGameId");

            migrationBuilder.AddForeignKey(
                name: "FK_BoardGameNights_BoardGames_SelectedBoardGameId",
                table: "BoardGameNights",
                column: "SelectedBoardGameId",
                principalTable: "BoardGames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoardGameNights_BoardGames_SelectedBoardGameId",
                table: "BoardGameNights");

            migrationBuilder.DropIndex(
                name: "IX_BoardGameNights_SelectedBoardGameId",
                table: "BoardGameNights");

            migrationBuilder.DropColumn(
                name: "JoinDateTime",
                table: "Player");

            migrationBuilder.AddColumn<int>(
                name: "BoardGameNightId1",
                table: "BoardGames",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BoardGames_BoardGameNightId1",
                table: "BoardGames",
                column: "BoardGameNightId1");

            migrationBuilder.AddForeignKey(
                name: "FK_BoardGames_BoardGameNights_BoardGameNightId1",
                table: "BoardGames",
                column: "BoardGameNightId1",
                principalTable: "BoardGameNights",
                principalColumn: "Id");
        }
    }
}
