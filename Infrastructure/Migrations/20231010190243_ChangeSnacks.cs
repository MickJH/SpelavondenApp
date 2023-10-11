﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeSnacks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Snacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BoardGameNightId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Snacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Snacks_BoardGameNights_BoardGameNightId",
                        column: x => x.BoardGameNightId,
                        principalTable: "BoardGameNights",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Snacks_BoardGameNightId",
                table: "Snacks",
                column: "BoardGameNightId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Snacks");
        }
    }
}
