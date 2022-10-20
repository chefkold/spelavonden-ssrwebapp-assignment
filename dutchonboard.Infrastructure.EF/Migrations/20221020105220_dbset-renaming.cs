using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dutchonboard.Infrastructure.EF.Migrations
{
    public partial class dbsetrenaming : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameNightPerson_Persons_PlayersId",
                table: "GameNightPerson");

            migrationBuilder.DropForeignKey(
                name: "FK_GameNights_Persons_HostId",
                table: "GameNights");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Persons",
                table: "Persons");

            migrationBuilder.RenameTable(
                name: "Persons",
                newName: "People");

            migrationBuilder.AddPrimaryKey(
                name: "PK_People",
                table: "People",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GameNightPerson_People_PlayersId",
                table: "GameNightPerson",
                column: "PlayersId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameNights_People_HostId",
                table: "GameNights",
                column: "HostId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameNightPerson_People_PlayersId",
                table: "GameNightPerson");

            migrationBuilder.DropForeignKey(
                name: "FK_GameNights_People_HostId",
                table: "GameNights");

            migrationBuilder.DropPrimaryKey(
                name: "PK_People",
                table: "People");

            migrationBuilder.RenameTable(
                name: "People",
                newName: "Persons");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Persons",
                table: "Persons",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GameNightPerson_Persons_PlayersId",
                table: "GameNightPerson",
                column: "PlayersId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameNights_Persons_HostId",
                table: "GameNights",
                column: "HostId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
