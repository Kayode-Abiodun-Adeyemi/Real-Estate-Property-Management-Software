using Microsoft.EntityFrameworkCore.Migrations;

namespace EstateManagementApp.Data.Migrations
{
    public partial class AddMorePropertytoBuilding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BuildingColor",
                table: "Buildings",
                newName: "BuyorSale");

            migrationBuilder.AlterColumn<string>(
                name: "PropertyAddress",
                table: "Buildings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BuyorSale",
                table: "Buildings",
                newName: "BuildingColor");

            migrationBuilder.AlterColumn<string>(
                name: "PropertyAddress",
                table: "Buildings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
