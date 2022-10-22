using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dutchonboard.Infrastructure.EF.Migrations
{
    public partial class renameddomainclass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BoardGames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardGames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameNights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdultOnly = table.Column<bool>(type: "bit", nullable: false),
                    MaxPlayerAmount = table.Column<int>(type: "int", nullable: false),
                    Location_Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location_Number = table.Column<int>(type: "int", nullable: false),
                    DateAndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HostId = table.Column<int>(type: "int", nullable: false),
                    DietAndAllergyInfo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameNights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameNights_Players_HostId",
                        column: x => x.HostId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BoardGameGameNight",
                columns: table => new
                {
                    GameNightsWhereFeaturedId = table.Column<int>(type: "int", nullable: false),
                    GamesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardGameGameNight", x => new { x.GameNightsWhereFeaturedId, x.GamesId });
                    table.ForeignKey(
                        name: "FK_BoardGameGameNight_BoardGames_GamesId",
                        column: x => x.GamesId,
                        principalTable: "BoardGames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoardGameGameNight_GameNights_GameNightsWhereFeaturedId",
                        column: x => x.GameNightsWhereFeaturedId,
                        principalTable: "GameNights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameNightPlayer",
                columns: table => new
                {
                    JoinedNightsId = table.Column<int>(type: "int", nullable: false),
                    PlayersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameNightPlayer", x => new { x.JoinedNightsId, x.PlayersId });
                    table.ForeignKey(
                        name: "FK_GameNightPlayer_GameNights_JoinedNightsId",
                        column: x => x.JoinedNightsId,
                        principalTable: "GameNights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameNightPlayer_Players_PlayersId",
                        column: x => x.PlayersId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoardGameGameNight_GamesId",
                table: "BoardGameGameNight",
                column: "GamesId");

            migrationBuilder.CreateIndex(
                name: "IX_GameNightPlayer_PlayersId",
                table: "GameNightPlayer",
                column: "PlayersId");

            migrationBuilder.CreateIndex(
                name: "IX_GameNights_HostId",
                table: "GameNights",
                column: "HostId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoardGameGameNight");

            migrationBuilder.DropTable(
                name: "GameNightPlayer");

            migrationBuilder.DropTable(
                name: "BoardGames");

            migrationBuilder.DropTable(
                name: "GameNights");

            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
