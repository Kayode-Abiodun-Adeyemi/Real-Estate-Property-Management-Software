using Microsoft.EntityFrameworkCore.Migrations;

namespace EstateManagementApp.Data.Migrations
{
    public partial class AddSinglePhotoPathChangeName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buildings_BuildingPhotoPaths_PhotoPathId",
                table: "Buildings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BuildingPhotoPaths",
                table: "BuildingPhotoPaths");

            migrationBuilder.RenameTable(
                name: "BuildingPhotoPaths",
                newName: "PhotoPath");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhotoPath",
                table: "PhotoPath",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Buildings_PhotoPath_PhotoPathId",
                table: "Buildings",
                column: "PhotoPathId",
                principalTable: "PhotoPath",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buildings_PhotoPath_PhotoPathId",
                table: "Buildings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PhotoPath",
                table: "PhotoPath");

            migrationBuilder.RenameTable(
                name: "PhotoPath",
                newName: "BuildingPhotoPaths");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BuildingPhotoPaths",
                table: "BuildingPhotoPaths",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Buildings_BuildingPhotoPaths_PhotoPathId",
                table: "Buildings",
                column: "PhotoPathId",
                principalTable: "BuildingPhotoPaths",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
