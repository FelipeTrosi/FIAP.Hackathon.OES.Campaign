using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FIAP.Hackathon.OES.Campaign.Infra.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalDonationsCollected",
                table: "Campaign",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalDonationsCollected",
                table: "Campaign");
        }
    }
}
