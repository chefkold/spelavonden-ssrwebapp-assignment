using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dutchonboard.Infrastructure.EF.Migrations
{
    public partial class modelupdatesincludingrenames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DietAndAllergyInfo",
                table: "GameNights",
                newName: "SupportedDietRestrictions");

            migrationBuilder.AddColumn<string>(
                name: "DietRestrictions",
                table: "Players",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DietRestrictions",
                table: "Players");

            migrationBuilder.RenameColumn(
                name: "SupportedDietRestrictions",
                table: "GameNights",
                newName: "DietAndAllergyInfo");
        }
    }
}
