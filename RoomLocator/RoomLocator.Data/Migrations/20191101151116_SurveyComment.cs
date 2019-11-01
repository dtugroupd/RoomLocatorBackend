using Microsoft.EntityFrameworkCore.Migrations;

namespace RoomLocator.Data.Migrations
{
    public partial class SurveyComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "Surveys",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "Surveys");
        }
    }
}
