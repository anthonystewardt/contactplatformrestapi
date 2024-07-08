﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace contactplatformweb.Migrations
{
    /// <inheritdoc />
    public partial class AddIsPostulateToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPostulate",
                table: "Users",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPostulate",
                table: "Users");
        }
    }
}
