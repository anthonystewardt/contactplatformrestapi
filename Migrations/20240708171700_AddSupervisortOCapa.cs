using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace contactplatformweb.Migrations
{
    /// <inheritdoc />
    public partial class AddSupervisortOCapa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SupervisorId",
                table: "Capas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Capas_SupervisorId",
                table: "Capas",
                column: "SupervisorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Capas_Users_SupervisorId",
                table: "Capas",
                column: "SupervisorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Capas_Users_SupervisorId",
                table: "Capas");

            migrationBuilder.DropIndex(
                name: "IX_Capas_SupervisorId",
                table: "Capas");

            migrationBuilder.DropColumn(
                name: "SupervisorId",
                table: "Capas");
        }
    }
}
