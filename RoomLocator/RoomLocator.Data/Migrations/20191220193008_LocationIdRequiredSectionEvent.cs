using Microsoft.EntityFrameworkCore.Migrations;

namespace RoomLocator.Data.Migrations
{
    public partial class LocationIdRequiredSectionEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coordinates_Locations_LocationId",
                table: "Coordinates");

            migrationBuilder.DropForeignKey(
                name: "FK_Coordinates_Sections_SectionId",
                table: "Coordinates");

            migrationBuilder.AlterColumn<string>(
                name: "LocationId",
                table: "Sections",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LocationId",
                table: "Events",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Coordinates_Locations_LocationId",
                table: "Coordinates",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Coordinates_Sections_SectionId",
                table: "Coordinates",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coordinates_Locations_LocationId",
                table: "Coordinates");

            migrationBuilder.DropForeignKey(
                name: "FK_Coordinates_Sections_SectionId",
                table: "Coordinates");

            migrationBuilder.AlterColumn<string>(
                name: "LocationId",
                table: "Sections",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LocationId",
                table: "Events",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_Coordinates_Locations_LocationId",
                table: "Coordinates",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Coordinates_Sections_SectionId",
                table: "Coordinates",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
