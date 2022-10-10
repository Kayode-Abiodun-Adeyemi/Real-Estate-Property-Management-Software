using Microsoft.EntityFrameworkCore.Migrations;

namespace EstateManagementApp.Data.Migrations
{
    public partial class AddBuildingPhotoPathThirdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhotoPath_Buildings_BuildingId",
                table: "PhotoPath");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PhotoPath",
                table: "PhotoPath");

            migrationBuilder.RenameTable(
                name: "PhotoPath",
                newName: "BuildingPhotoPaths");

            migrationBuilder.RenameIndex(
                name: "IX_PhotoPath_BuildingId",
                table: "BuildingPhotoPaths",
                newName: "IX_BuildingPhotoPaths_BuildingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BuildingPhotoPaths",
                table: "BuildingPhotoPaths",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BuildingPhotoPaths_Buildings_BuildingId",
                table: "BuildingPhotoPaths",
                column: "BuildingId",
                principalTable: "Buildings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BuildingPhotoPaths_Buildings_BuildingId",
                table: "BuildingPhotoPaths");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BuildingPhotoPaths",
                table: "BuildingPhotoPaths");

            migrationBuilder.RenameTable(
                name: "BuildingPhotoPaths",
                newName: "PhotoPath");

            migrationBuilder.RenameIndex(
                name: "IX_BuildingPhotoPaths_BuildingId",
                table: "PhotoPath",
                newName: "IX_PhotoPath_BuildingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhotoPath",
                table: "PhotoPath",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoPath_Buildings_BuildingId",
                table: "PhotoPath",
                column: "BuildingId",
                principalTable: "Buildings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
