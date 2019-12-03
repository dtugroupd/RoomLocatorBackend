using Microsoft.EntityFrameworkCore.Migrations;

namespace RoomLocator.Data.Migrations
{
    public partial class ChangedSensorModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExternalId",
                table: "Sensors",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "Sensors");
        }
    }
}
