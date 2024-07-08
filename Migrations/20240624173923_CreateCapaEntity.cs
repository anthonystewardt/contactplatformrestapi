using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace contactplatformweb.Migrations
{
    /// <inheritdoc />
    public partial class CreateCapaEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CapaId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Capas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CampaignId = table.Column<int>(type: "int", nullable: false),
                    supervisorId = table.Column<int>(type: "int", nullable: false),
                    DateStart = table.Column<DateOnly>(type: "date", nullable: false),
                    DateEnd = table.Column<DateOnly>(type: "date", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Capas", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_CapaId",
                table: "Users",
                column: "CapaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Capas_CapaId",
                table: "Users",
                column: "CapaId",
                principalTable: "Capas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Capas_CapaId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Capas");

            migrationBuilder.DropIndex(
                name: "IX_Users_CapaId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CapaId",
                table: "Users");
        }
    }
}
