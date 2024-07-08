using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace contactplatformweb.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnsToWeek : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Weeks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Friday",
                table: "Weeks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "FridayIsActive",
                table: "Weeks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Monday",
                table: "Weeks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "MondayIsActive",
                table: "Weeks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Saturday",
                table: "Weeks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "SaturdayIsActive",
                table: "Weeks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Sunday",
                table: "Weeks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "SundayIsActive",
                table: "Weeks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Thursday",
                table: "Weeks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "ThursdayIsActive",
                table: "Weeks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Tuesday",
                table: "Weeks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "TuesdayIsActive",
                table: "Weeks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Weeks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Wednesday",
                table: "Weeks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Weeks");

            migrationBuilder.DropColumn(
                name: "Friday",
                table: "Weeks");

            migrationBuilder.DropColumn(
                name: "FridayIsActive",
                table: "Weeks");

            migrationBuilder.DropColumn(
                name: "Monday",
                table: "Weeks");

            migrationBuilder.DropColumn(
                name: "MondayIsActive",
                table: "Weeks");

            migrationBuilder.DropColumn(
                name: "Saturday",
                table: "Weeks");

            migrationBuilder.DropColumn(
                name: "SaturdayIsActive",
                table: "Weeks");

            migrationBuilder.DropColumn(
                name: "Sunday",
                table: "Weeks");

            migrationBuilder.DropColumn(
                name: "SundayIsActive",
                table: "Weeks");

            migrationBuilder.DropColumn(
                name: "Thursday",
                table: "Weeks");

            migrationBuilder.DropColumn(
                name: "ThursdayIsActive",
                table: "Weeks");

            migrationBuilder.DropColumn(
                name: "Tuesday",
                table: "Weeks");

            migrationBuilder.DropColumn(
                name: "TuesdayIsActive",
                table: "Weeks");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Weeks");

            migrationBuilder.DropColumn(
                name: "Wednesday",
                table: "Weeks");
        }
    }
}
