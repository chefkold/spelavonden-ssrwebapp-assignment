using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dutchonboard.Infrastructure.EF.Migrations
{
    public partial class cascaderulesplayersgamenights : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameNights_Players_HostId",
                table: "GameNights");

            migrationBuilder.AddForeignKey(
                name: "FK_GameNights_Players_HostId",
                table: "GameNights",
                column: "HostId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameNights_Players_HostId",
                table: "GameNights");

            migrationBuilder.AddForeignKey(
                name: "FK_GameNights_Players_HostId",
                table: "GameNights",
                column: "HostId",
                principalTable: "Players",
                principalColumn: "Id");
        }
    }
}
