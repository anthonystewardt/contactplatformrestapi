using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace contactplatformweb.Migrations
{
    /// <inheritdoc />
    public partial class AddTimeSpanWeekByDay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "FridayEndTime",
                table: "Weeks",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "FridayStartTime",
                table: "Weeks",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "MondayEndTime",
                table: "Weeks",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "MondayStartTime",
                table: "Weeks",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "SaturdayEndTime",
                table: "Weeks",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "SaturdayStartTime",
                table: "Weeks",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "SundayEndTime",
                table: "Weeks",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "SundayStartTime",
                table: "Weeks",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "ThursdayEndTime",
                table: "Weeks",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "ThursdayStartTime",
                table: "Weeks",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TuesdayEndTime",
                table: "Weeks",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TuesdayStartTime",
                table: "Weeks",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "WednesdayEndTime",
                table: "Weeks",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "WednesdayStartTime",
                table: "Weeks",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FridayEndTime",
                table: "Weeks");

            migrationBuilder.DropColumn(
                name: "FridayStartTime",
                table: "Weeks");

            migrationBuilder.DropColumn(
                name: "MondayEndTime",
                table: "Weeks");

            migrationBuilder.DropColumn(
                name: "MondayStartTime",
                table: "Weeks");

            migrationBuilder.DropColumn(
                name: "SaturdayEndTime",
                table: "Weeks");

            migrationBuilder.DropColumn(
                name: "SaturdayStartTime",
                table: "Weeks");

            migrationBuilder.DropColumn(
                name: "SundayEndTime",
                table: "Weeks");

            migrationBuilder.DropColumn(
                name: "SundayStartTime",
                table: "Weeks");

            migrationBuilder.DropColumn(
                name: "ThursdayEndTime",
                table: "Weeks");

            migrationBuilder.DropColumn(
                name: "ThursdayStartTime",
                table: "Weeks");

            migrationBuilder.DropColumn(
                name: "TuesdayEndTime",
                table: "Weeks");

            migrationBuilder.DropColumn(
                name: "TuesdayStartTime",
                table: "Weeks");

            migrationBuilder.DropColumn(
                name: "WednesdayEndTime",
                table: "Weeks");

            migrationBuilder.DropColumn(
                name: "WednesdayStartTime",
                table: "Weeks");
        }
    }
}
