using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dutchonboard.Infrastructure.EF.Migrations
{
    public partial class consumptiongamenightrelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consumption_GameNights_GameNightId",
                table: "Consumption");

            migrationBuilder.DropIndex(
                name: "IX_Consumption_GameNightId",
                table: "Consumption");

            migrationBuilder.DropColumn(
                name: "GameNightId",
                table: "Consumption");

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

            migrationBuilder.CreateIndex(
                name: "IX_ConsumptionGameNight_GameNightsWhereConsumedId",
                table: "ConsumptionGameNight",
                column: "GameNightsWhereConsumedId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConsumptionGameNight");

            migrationBuilder.AddColumn<int>(
                name: "GameNightId",
                table: "Consumption",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Consumption_GameNightId",
                table: "Consumption",
                column: "GameNightId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumption_GameNights_GameNightId",
                table: "Consumption",
                column: "GameNightId",
                principalTable: "GameNights",
                principalColumn: "Id");
        }
    }
}
