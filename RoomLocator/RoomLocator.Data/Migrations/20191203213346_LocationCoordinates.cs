using Microsoft.EntityFrameworkCore.Migrations;

namespace RoomLocator.Data.Migrations
{
    public partial class LocationCoordinates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LocationId",
                table: "Coordinates",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Coordinates_LocationId",
                table: "Coordinates",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Coordinates_Locations_LocationId",
                table: "Coordinates",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coordinates_Locations_LocationId",
                table: "Coordinates");

            migrationBuilder.DropIndex(
                name: "IX_Coordinates_LocationId",
                table: "Coordinates");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Coordinates");
        }
    }
}
