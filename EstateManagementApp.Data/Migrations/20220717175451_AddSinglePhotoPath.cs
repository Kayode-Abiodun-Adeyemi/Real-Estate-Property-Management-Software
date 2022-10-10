using Microsoft.EntityFrameworkCore.Migrations;

namespace EstateManagementApp.Data.Migrations
{
    public partial class AddSinglePhotoPath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BuildingPhotoPaths_Buildings_BuildingId",
                table: "BuildingPhotoPaths");

            migrationBuilder.DropIndex(
                name: "IX_BuildingPhotoPaths_BuildingId",
                table: "BuildingPhotoPaths");

            migrationBuilder.DropColumn(
                name: "BuildingId",
                table: "BuildingPhotoPaths");

            migrationBuilder.AddColumn<int>(
                name: "PhotoPathId",
                table: "Buildings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_PhotoPathId",
                table: "Buildings",
                column: "PhotoPathId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Buildings_BuildingPhotoPaths_PhotoPathId",
                table: "Buildings",
                column: "PhotoPathId",
                principalTable: "BuildingPhotoPaths",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buildings_BuildingPhotoPaths_PhotoPathId",
                table: "Buildings");

            migrationBuilder.DropIndex(
                name: "IX_Buildings_PhotoPathId",
                table: "Buildings");

            migrationBuilder.DropColumn(
                name: "PhotoPathId",
                table: "Buildings");

            migrationBuilder.AddColumn<int>(
                name: "BuildingId",
                table: "BuildingPhotoPaths",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BuildingPhotoPaths_BuildingId",
                table: "BuildingPhotoPaths",
                column: "BuildingId");

            migrationBuilder.AddForeignKey(
                name: "FK_BuildingPhotoPaths_Buildings_BuildingId",
                table: "BuildingPhotoPaths",
                column: "BuildingId",
                principalTable: "Buildings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
