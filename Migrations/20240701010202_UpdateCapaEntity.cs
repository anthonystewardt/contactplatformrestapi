using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace contactplatformweb.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCapaEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubcampaignId",
                table: "Capas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "trainerId",
                table: "Capas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubcampaignId",
                table: "Capas");

            migrationBuilder.DropColumn(
                name: "trainerId",
                table: "Capas");
        }
    }
}
