using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace contactplatformweb.Migrations
{
    /// <inheritdoc />
    public partial class AddModalityIdToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ModalityId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ModalityId",
                table: "Users",
                column: "ModalityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Modalities_ModalityId",
                table: "Users",
                column: "ModalityId",
                principalTable: "Modalities",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Modalities_ModalityId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ModalityId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ModalityId",
                table: "Users");
        }
    }
}
