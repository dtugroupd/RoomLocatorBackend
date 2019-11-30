using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RoomLocator.Data.Migrations
{
    public partial class Sensor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "TimeStamp",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "Sensors");

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Sensors",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Provider",
                table: "Sensors",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ZLevel",
                table: "Sensors",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Provider",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "ZLevel",
                table: "Sensors");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Sensors",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Sensors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Sensors",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeStamp",
                table: "Sensors",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "Sensors",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Value",
                table: "Sensors",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
