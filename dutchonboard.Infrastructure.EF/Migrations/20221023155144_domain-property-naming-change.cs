using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dutchonboard.Infrastructure.EF.Migrations
{
    public partial class domainpropertynamingchange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameNights_Players_HostId",
                table: "GameNights");

            migrationBuilder.RenameColumn(
                name: "HostId",
                table: "GameNights",
                newName: "OrganizerId");

            migrationBuilder.RenameIndex(
                name: "IX_GameNights_HostId",
                table: "GameNights",
                newName: "IX_GameNights_OrganizerId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameNights_Players_OrganizerId",
                table: "GameNights",
                column: "OrganizerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameNights_Players_OrganizerId",
                table: "GameNights");

            migrationBuilder.RenameColumn(
                name: "OrganizerId",
                table: "GameNights",
                newName: "HostId");

            migrationBuilder.RenameIndex(
                name: "IX_GameNights_OrganizerId",
                table: "GameNights",
                newName: "IX_GameNights_HostId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameNights_Players_HostId",
                table: "GameNights",
                column: "HostId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
