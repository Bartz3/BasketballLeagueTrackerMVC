using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasketballLeagueTracker.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class userFavouritesTLP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavouritePlayers_Players_PlayerId",
                table: "FavouritePlayers");

            migrationBuilder.DropForeignKey(
                name: "FK_FavouriteTeams_AspNetUsers_UserId",
                table: "FavouriteTeams");

            migrationBuilder.DropForeignKey(
                name: "FK_FavouriteTeams_Teams_TeamId",
                table: "FavouriteTeams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavouritePlayers",
                table: "FavouritePlayers");

            migrationBuilder.DropIndex(
                name: "IX_FavouritePlayers_UserId",
                table: "FavouritePlayers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavouriteTeams",
                table: "FavouriteTeams");

            migrationBuilder.DropIndex(
                name: "IX_FavouriteTeams_UserId",
                table: "FavouriteTeams");

            migrationBuilder.DropColumn(
                name: "FavouritePlayerId",
                table: "FavouritePlayers");

            migrationBuilder.DropColumn(
                name: "FavouriteTeamId",
                table: "FavouriteTeams");

            migrationBuilder.RenameTable(
                name: "FavouriteTeams",
                newName: "FavouriteTeam");

            migrationBuilder.RenameIndex(
                name: "IX_FavouriteTeams_TeamId",
                table: "FavouriteTeam",
                newName: "IX_FavouriteTeam_TeamId");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Logo",
                table: "Leagues",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Leagues",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateAdded",
                table: "FavouritePlayers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateAdded",
                table: "FavouriteTeam",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "TeamId1",
                table: "FavouriteTeam",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavouritePlayers",
                table: "FavouritePlayers",
                columns: new[] { "UserId", "PlayerId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavouriteTeam",
                table: "FavouriteTeam",
                columns: new[] { "UserId", "TeamId" });

            migrationBuilder.CreateTable(
                name: "FavouriteLeague",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LeagueId = table.Column<int>(type: "int", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavouriteLeague", x => new { x.UserId, x.LeagueId });
                    table.ForeignKey(
                        name: "FK_FavouriteLeague_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavouriteLeague_Leagues_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "Leagues",
                        principalColumn: "LeagueId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FavouriteTeam_TeamId1",
                table: "FavouriteTeam",
                column: "TeamId1");

            migrationBuilder.CreateIndex(
                name: "IX_FavouriteLeague_LeagueId",
                table: "FavouriteLeague",
                column: "LeagueId");

            migrationBuilder.AddForeignKey(
                name: "FK_FavouritePlayers_Players_PlayerId",
                table: "FavouritePlayers",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "PlayerId",
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

            migrationBuilder.AddForeignKey(
                name: "FK_FavouriteTeam_Teams_TeamId1",
                table: "FavouriteTeam",
                column: "TeamId1",
                principalTable: "Teams",
                principalColumn: "TeamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavouritePlayers_Players_PlayerId",
                table: "FavouritePlayers");

            migrationBuilder.DropForeignKey(
                name: "FK_FavouriteTeam_AspNetUsers_UserId",
                table: "FavouriteTeam");

            migrationBuilder.DropForeignKey(
                name: "FK_FavouriteTeam_Teams_TeamId",
                table: "FavouriteTeam");

            migrationBuilder.DropForeignKey(
                name: "FK_FavouriteTeam_Teams_TeamId1",
                table: "FavouriteTeam");

            migrationBuilder.DropTable(
                name: "FavouriteLeague");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavouritePlayers",
                table: "FavouritePlayers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavouriteTeam",
                table: "FavouriteTeam");

            migrationBuilder.DropIndex(
                name: "IX_FavouriteTeam_TeamId1",
                table: "FavouriteTeam");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Leagues");

            migrationBuilder.DropColumn(
                name: "DateAdded",
                table: "FavouritePlayers");

            migrationBuilder.DropColumn(
                name: "DateAdded",
                table: "FavouriteTeam");

            migrationBuilder.DropColumn(
                name: "TeamId1",
                table: "FavouriteTeam");

            migrationBuilder.RenameTable(
                name: "FavouriteTeam",
                newName: "FavouriteTeams");

            migrationBuilder.RenameIndex(
                name: "IX_FavouriteTeam_TeamId",
                table: "FavouriteTeams",
                newName: "IX_FavouriteTeams_TeamId");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Logo",
                table: "Leagues",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FavouritePlayerId",
                table: "FavouritePlayers",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "FavouriteTeamId",
                table: "FavouriteTeams",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavouritePlayers",
                table: "FavouritePlayers",
                column: "FavouritePlayerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavouriteTeams",
                table: "FavouriteTeams",
                column: "FavouriteTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_FavouritePlayers_UserId",
                table: "FavouritePlayers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FavouriteTeams_UserId",
                table: "FavouriteTeams",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FavouritePlayers_Players_PlayerId",
                table: "FavouritePlayers",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "PlayerId");

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
                principalColumn: "TeamId");
        }
    }
}
