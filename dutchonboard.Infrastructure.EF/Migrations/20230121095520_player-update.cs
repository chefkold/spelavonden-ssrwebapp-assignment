using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dutchonboard.Infrastructure.EF.Migrations
{
    public partial class playerupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address_City",
                table: "Players",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Address_Number",
                table: "Players",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_Street",
                table: "Players",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<bool>(
                name: "IsForAdults",
                table: "GameNights",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address_City",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Address_Number",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Address_Street",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Players");

            migrationBuilder.AlterColumn<bool>(
                name: "IsForAdults",
                table: "GameNights",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");
        }
    }
}
