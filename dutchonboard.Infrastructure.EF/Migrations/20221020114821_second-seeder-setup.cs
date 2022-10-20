using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dutchonboard.Infrastructure.EF.Migrations
{
    public partial class secondseedersetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoardGameGameNight_BoardGame_GamesId",
                table: "BoardGameGameNight");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BoardGame",
                table: "BoardGame");

            migrationBuilder.RenameTable(
                name: "BoardGame",
                newName: "BoardGames");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BoardGames",
                table: "BoardGames",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BoardGameGameNight_BoardGames_GamesId",
                table: "BoardGameGameNight",
                column: "GamesId",
                principalTable: "BoardGames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoardGameGameNight_BoardGames_GamesId",
                table: "BoardGameGameNight");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BoardGames",
                table: "BoardGames");

            migrationBuilder.RenameTable(
                name: "BoardGames",
                newName: "BoardGame");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BoardGame",
                table: "BoardGame",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BoardGameGameNight_BoardGame_GamesId",
                table: "BoardGameGameNight",
                column: "GamesId",
                principalTable: "BoardGame",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
