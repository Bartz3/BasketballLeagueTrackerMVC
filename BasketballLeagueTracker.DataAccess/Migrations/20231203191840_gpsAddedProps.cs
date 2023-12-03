using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasketballLeagueTracker.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class gpsAddedProps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "FGA",
                table: "GamePlayerStats",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "FGM",
                table: "GamePlayerStats",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "FTA",
                table: "GamePlayerStats",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "FTM",
                table: "GamePlayerStats",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "PA3",
                table: "GamePlayerStats",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "PM3",
                table: "GamePlayerStats",
                type: "smallint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FGA",
                table: "GamePlayerStats");

            migrationBuilder.DropColumn(
                name: "FGM",
                table: "GamePlayerStats");

            migrationBuilder.DropColumn(
                name: "FTA",
                table: "GamePlayerStats");

            migrationBuilder.DropColumn(
                name: "FTM",
                table: "GamePlayerStats");

            migrationBuilder.DropColumn(
                name: "PA3",
                table: "GamePlayerStats");

            migrationBuilder.DropColumn(
                name: "PM3",
                table: "GamePlayerStats");
        }
    }
}
