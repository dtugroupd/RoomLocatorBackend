using Microsoft.EntityFrameworkCore.Migrations;

namespace RoomLocator.Data.Migrations
{
    public partial class AddUserDisclaimers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserDisclaimers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    HasAcceptedDisclaimer = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDisclaimers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserDisclaimers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserDisclaimers_UserId",
                table: "UserDisclaimers",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserDisclaimers");
        }
    }
}
