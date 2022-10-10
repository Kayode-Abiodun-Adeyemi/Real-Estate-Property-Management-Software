using Microsoft.EntityFrameworkCore.Migrations;

namespace EstateManagementApp.Data.Migrations
{
    public partial class MigrationforPhotoPath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buildings_PhotoPath_PhotoPathId",
                table: "Buildings");

            migrationBuilder.DropIndex(
                name: "IX_Buildings_PhotoPathId",
                table: "Buildings");

            migrationBuilder.DropColumn(
                name: "PhotoPathId",
                table: "Buildings");

            migrationBuilder.AddColumn<int>(
                name: "BuildingId",
                table: "PhotoPath",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                table: "Buildings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoPath_BuildingId",
                table: "PhotoPath",
                column: "BuildingId");

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoPath_Buildings_BuildingId",
                table: "PhotoPath",
                column: "BuildingId",
                principalTable: "Buildings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhotoPath_Buildings_BuildingId",
                table: "PhotoPath");

            migrationBuilder.DropIndex(
                name: "IX_PhotoPath_BuildingId",
                table: "PhotoPath");

            migrationBuilder.DropColumn(
                name: "BuildingId",
                table: "PhotoPath");

            migrationBuilder.DropColumn(
                name: "PhotoPath",
                table: "Buildings");

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
                name: "FK_Buildings_PhotoPath_PhotoPathId",
                table: "Buildings",
                column: "PhotoPathId",
                principalTable: "PhotoPath",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
