using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dutchonboard.Infrastructure.EF.Migrations
{
    public partial class cleaninit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BoardGames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Genre = table.Column<int>(type: "int", nullable: false),
                    IsForAdults = table.Column<bool>(type: "bit", nullable: true),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ImageFormat = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardGames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Consumption",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DietRestrictions = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consumption", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Address_Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_Number = table.Column<int>(type: "int", nullable: true),
                    Address_City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DietRestrictions = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxPlayerAmount = table.Column<int>(type: "int", nullable: false),
                    Potluck = table.Column<bool>(type: "bit", nullable: false),
                    IsForAdults = table.Column<bool>(type: "bit", nullable: false),
                    OrganizerId = table.Column<int>(type: "int", nullable: true),
                    Location_Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location_Number = table.Column<int>(type: "int", nullable: true),
                    Location_City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateAndTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameNights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameNights_Players_OrganizerId",
                        column: x => x.OrganizerId,
                        principalTable: "Players",
                        principalColumn: "Id");
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
                name: "ConsumptionGameNight",
                columns: table => new
                {
                    ConsumptionsId = table.Column<int>(type: "int", nullable: false),
                    GameNightsWhereConsumedId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumptionGameNight", x => new { x.ConsumptionsId, x.GameNightsWhereConsumedId });
                    table.ForeignKey(
                        name: "FK_ConsumptionGameNight_Consumption_ConsumptionsId",
                        column: x => x.ConsumptionsId,
                        principalTable: "Consumption",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConsumptionGameNight_GameNights_GameNightsWhereConsumedId",
                        column: x => x.GameNightsWhereConsumedId,
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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoardGameGameNight_GamesId",
                table: "BoardGameGameNight",
                column: "GamesId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsumptionGameNight_GameNightsWhereConsumedId",
                table: "ConsumptionGameNight",
                column: "GameNightsWhereConsumedId");

            migrationBuilder.CreateIndex(
                name: "IX_GameNightPlayer_PlayersId",
                table: "GameNightPlayer",
                column: "PlayersId");

            migrationBuilder.CreateIndex(
                name: "IX_GameNights_OrganizerId",
                table: "GameNights",
                column: "OrganizerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoardGameGameNight");

            migrationBuilder.DropTable(
                name: "ConsumptionGameNight");

            migrationBuilder.DropTable(
                name: "GameNightPlayer");

            migrationBuilder.DropTable(
                name: "BoardGames");

            migrationBuilder.DropTable(
                name: "Consumption");

            migrationBuilder.DropTable(
                name: "GameNights");

            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
