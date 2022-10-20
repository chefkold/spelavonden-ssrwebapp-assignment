using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dutchonboard.Infrastructure.EF.Migrations
{
    public partial class addedenumconversion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_GameNights_GameNightId",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_GameNightId",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "GameNightId",
                table: "Persons");

            migrationBuilder.AddColumn<string>(
                name: "DietAndAllergyInfo",
                table: "GameNights",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "GameNightPerson",
                columns: table => new
                {
                    JoinedNightsId = table.Column<int>(type: "int", nullable: false),
                    PlayersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameNightPerson", x => new { x.JoinedNightsId, x.PlayersId });
                    table.ForeignKey(
                        name: "FK_GameNightPerson_GameNights_JoinedNightsId",
                        column: x => x.JoinedNightsId,
                        principalTable: "GameNights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_GameNightPerson_Persons_PlayersId",
                        column: x => x.PlayersId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameNightPerson_PlayersId",
                table: "GameNightPerson",
                column: "PlayersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameNightPerson");

            migrationBuilder.DropColumn(
                name: "DietAndAllergyInfo",
                table: "GameNights");

            migrationBuilder.AddColumn<int>(
                name: "GameNightId",
                table: "Persons",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Persons_GameNightId",
                table: "Persons",
                column: "GameNightId");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_GameNights_GameNightId",
                table: "Persons",
                column: "GameNightId",
                principalTable: "GameNights",
                principalColumn: "Id");
        }
    }
}
