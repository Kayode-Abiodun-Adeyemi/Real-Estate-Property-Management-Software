using Microsoft.EntityFrameworkCore.Migrations;

namespace EstateManagementApp.Data.Migrations
{
    public partial class AddMoreFieldsToBuildingFifthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LandLordEmailAddress",
                table: "Buildings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LandLordFullName",
                table: "Buildings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PropertyAddress",
                table: "Buildings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LandLordEmailAddress",
                table: "Buildings");

            migrationBuilder.DropColumn(
                name: "LandLordFullName",
                table: "Buildings");

            migrationBuilder.DropColumn(
                name: "PropertyAddress",
                table: "Buildings");
        }
    }
}
