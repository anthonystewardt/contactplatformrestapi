using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace contactplatformweb.Migrations
{
    /// <inheritdoc />
    public partial class NewColumnCapaId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CapaUsers");

            migrationBuilder.AddColumn<int>(
                name: "CapaId",
                table: "Users",
                type: "int",
                nullable: true);

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

            migrationBuilder.DropIndex(
                name: "IX_Users_CapaId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CapaId",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "CapaUsers",
                columns: table => new
                {
                    CapaId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CapaUsers", x => new { x.CapaId, x.UserId });
                    table.ForeignKey(
                        name: "FK_CapaUsers_Capas_CapaId",
                        column: x => x.CapaId,
                        principalTable: "Capas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CapaUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CapaUsers_UserId",
                table: "CapaUsers",
                column: "UserId");
        }
    }
}
