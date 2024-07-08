using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace contactplatformweb.Migrations
{
    /// <inheritdoc />
    public partial class Trainer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_SubCampaigns_CampaignId",
                table: "SubCampaigns",
                column: "CampaignId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubCampaigns_Campaigns_CampaignId",
                table: "SubCampaigns",
                column: "CampaignId",
                principalTable: "Campaigns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubCampaigns_Campaigns_CampaignId",
                table: "SubCampaigns");

            migrationBuilder.DropIndex(
                name: "IX_SubCampaigns_CampaignId",
                table: "SubCampaigns");
        }
    }
}
