using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasketballLeagueTracker.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class favteams : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavouriteLeague_AspNetUsers_UserId",
                table: "FavouriteLeague");

            migrationBuilder.DropForeignKey(
                name: "FK_FavouriteLeague_Leagues_LeagueId",
                table: "FavouriteLeague");

            migrationBuilder.DropForeignKey(
                name: "FK_FavouriteTeam_AspNetUsers_UserId",
                table: "FavouriteTeam");

            migrationBuilder.DropForeignKey(
                name: "FK_FavouriteTeam_Teams_TeamId",
                table: "FavouriteTeam");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavouriteTeam",
                table: "FavouriteTeam");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavouriteLeague",
                table: "FavouriteLeague");

            migrationBuilder.DropColumn(
                name: "StadiumLatitude",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "StadiumLongitude",
                table: "Teams");

            migrationBuilder.RenameTable(
                name: "FavouriteTeam",
                newName: "FavouriteTeams");

            migrationBuilder.RenameTable(
                name: "FavouriteLeague",
                newName: "FavouriteLeagues");

            migrationBuilder.RenameIndex(
                name: "IX_FavouriteTeam_TeamId",
                table: "FavouriteTeams",
                newName: "IX_FavouriteTeams_TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_FavouriteLeague_LeagueId",
                table: "FavouriteLeagues",
                newName: "IX_FavouriteLeagues_LeagueId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavouriteTeams",
                table: "FavouriteTeams",
                columns: new[] { "UserId", "TeamId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavouriteLeagues",
                table: "FavouriteLeagues",
                columns: new[] { "UserId", "LeagueId" });

            migrationBuilder.AddForeignKey(
                name: "FK_FavouriteLeagues_AspNetUsers_UserId",
                table: "FavouriteLeagues",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavouriteLeagues_Leagues_LeagueId",
                table: "FavouriteLeagues",
                column: "LeagueId",
                principalTable: "Leagues",
                principalColumn: "LeagueId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavouriteTeams_AspNetUsers_UserId",
                table: "FavouriteTeams",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavouriteTeams_Teams_TeamId",
                table: "FavouriteTeams",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "TeamId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavouriteLeagues_AspNetUsers_UserId",
                table: "FavouriteLeagues");

            migrationBuilder.DropForeignKey(
                name: "FK_FavouriteLeagues_Leagues_LeagueId",
                table: "FavouriteLeagues");

            migrationBuilder.DropForeignKey(
                name: "FK_FavouriteTeams_AspNetUsers_UserId",
                table: "FavouriteTeams");

            migrationBuilder.DropForeignKey(
                name: "FK_FavouriteTeams_Teams_TeamId",
                table: "FavouriteTeams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavouriteTeams",
                table: "FavouriteTeams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavouriteLeagues",
                table: "FavouriteLeagues");

            migrationBuilder.RenameTable(
                name: "FavouriteTeams",
                newName: "FavouriteTeam");

            migrationBuilder.RenameTable(
                name: "FavouriteLeagues",
                newName: "FavouriteLeague");

            migrationBuilder.RenameIndex(
                name: "IX_FavouriteTeams_TeamId",
                table: "FavouriteTeam",
                newName: "IX_FavouriteTeam_TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_FavouriteLeagues_LeagueId",
                table: "FavouriteLeague",
                newName: "IX_FavouriteLeague_LeagueId");

            migrationBuilder.AddColumn<string>(
                name: "StadiumLatitude",
                table: "Teams",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StadiumLongitude",
                table: "Teams",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavouriteTeam",
                table: "FavouriteTeam",
                columns: new[] { "UserId", "TeamId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavouriteLeague",
                table: "FavouriteLeague",
                columns: new[] { "UserId", "LeagueId" });

            migrationBuilder.AddForeignKey(
                name: "FK_FavouriteLeague_AspNetUsers_UserId",
                table: "FavouriteLeague",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavouriteLeague_Leagues_LeagueId",
                table: "FavouriteLeague",
                column: "LeagueId",
                principalTable: "Leagues",
                principalColumn: "LeagueId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavouriteTeam_AspNetUsers_UserId",
                table: "FavouriteTeam",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavouriteTeam_Teams_TeamId",
                table: "FavouriteTeam",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "TeamId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
