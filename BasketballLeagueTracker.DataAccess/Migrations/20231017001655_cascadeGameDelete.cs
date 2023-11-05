using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasketballLeagueTracker.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class cascadeGameDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GamePlayerStats_Games_GameId",
                table: "GamePlayerStats");

            migrationBuilder.AddForeignKey(
                name: "FK_GamePlayerStats_Games_GameId",
                table: "GamePlayerStats",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "GameId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GamePlayerStats_Games_GameId",
                table: "GamePlayerStats");

            migrationBuilder.AddForeignKey(
                name: "FK_GamePlayerStats_Games_GameId",
                table: "GamePlayerStats",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "GameId");
        }
    }
}
