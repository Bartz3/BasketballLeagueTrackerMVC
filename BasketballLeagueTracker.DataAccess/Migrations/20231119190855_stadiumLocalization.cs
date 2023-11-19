using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasketballLeagueTracker.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class stadiumLocalization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "StadiumLatitude",
                table: "Teams",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "StadiumLongitude",
                table: "Teams",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StadiumLatitude",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "StadiumLongitude",
                table: "Teams");
        }
    }
}
