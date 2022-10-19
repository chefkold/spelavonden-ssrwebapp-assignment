using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dutchonboard.Infrastructure.EF.Migrations
{
    public partial class manytomanycorrection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoardGame_GameNights_GameNightId",
                table: "BoardGame");

            migrationBuilder.DropIndex(
                name: "IX_BoardGame_GameNightId",
                table: "BoardGame");

            migrationBuilder.DropColumn(
                name: "GameNightId",
                table: "BoardGame");

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
                        name: "FK_BoardGameGameNight_BoardGame_GamesId",
                        column: x => x.GamesId,
                        principalTable: "BoardGame",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoardGameGameNight_GameNights_GameNightsWhereFeaturedId",
                        column: x => x.GameNightsWhereFeaturedId,
                        principalTable: "GameNights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoardGameGameNight_GamesId",
                table: "BoardGameGameNight",
                column: "GamesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoardGameGameNight");

            migrationBuilder.AddColumn<int>(
                name: "GameNightId",
                table: "BoardGame",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BoardGame_GameNightId",
                table: "BoardGame",
                column: "GameNightId");

            migrationBuilder.AddForeignKey(
                name: "FK_BoardGame_GameNights_GameNightId",
                table: "BoardGame",
                column: "GameNightId",
                principalTable: "GameNights",
                principalColumn: "Id");
        }
    }
}
