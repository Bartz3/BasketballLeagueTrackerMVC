using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasketballLeagueTracker.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class teamFavTeamIdFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavouriteTeam_Teams_TeamId1",
                table: "FavouriteTeam");

            migrationBuilder.DropIndex(
                name: "IX_FavouriteTeam_TeamId1",
                table: "FavouriteTeam");

            migrationBuilder.DropColumn(
                name: "TeamId1",
                table: "FavouriteTeam");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeamId1",
                table: "FavouriteTeam",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FavouriteTeam_TeamId1",
                table: "FavouriteTeam",
                column: "TeamId1");

            migrationBuilder.AddForeignKey(
                name: "FK_FavouriteTeam_Teams_TeamId1",
                table: "FavouriteTeam",
                column: "TeamId1",
                principalTable: "Teams",
                principalColumn: "TeamId");
        }
    }
}
