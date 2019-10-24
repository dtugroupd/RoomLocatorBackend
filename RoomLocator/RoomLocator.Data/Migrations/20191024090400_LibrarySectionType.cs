using Microsoft.EntityFrameworkCore.Migrations;

namespace RoomLocator.Data.Migrations
{
    public partial class LibrarySectionType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "MazeMapSections",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "MazeMapSections");
        }
    }
}
