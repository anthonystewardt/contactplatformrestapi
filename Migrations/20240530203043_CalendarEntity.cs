using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace contactplatformweb.Migrations
{
    /// <inheritdoc />
    public partial class CalendarEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CalendarId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PositionId1",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Calendars",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Monday = table.Column<bool>(type: "bit", nullable: false),
                    Tuesday = table.Column<bool>(type: "bit", nullable: false),
                    Wednesday = table.Column<bool>(type: "bit", nullable: false),
                    Thursday = table.Column<bool>(type: "bit", nullable: false),
                    Friday = table.Column<bool>(type: "bit", nullable: false),
                    Saturday = table.Column<bool>(type: "bit", nullable: false),
                    Sunday = table.Column<bool>(type: "bit", nullable: false),
                    StartHour = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndHour = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HourOfDay = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calendars", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_CalendarId",
                table: "Users",
                column: "CalendarId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PositionId1",
                table: "Users",
                column: "PositionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Calendars_CalendarId",
                table: "Users",
                column: "CalendarId",
                principalTable: "Calendars",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Positions_PositionId1",
                table: "Users",
                column: "PositionId1",
                principalTable: "Positions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Calendars_CalendarId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Positions_PositionId1",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Calendars");

            migrationBuilder.DropIndex(
                name: "IX_Users_CalendarId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_PositionId1",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CalendarId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PositionId1",
                table: "Users");
        }
    }
}
