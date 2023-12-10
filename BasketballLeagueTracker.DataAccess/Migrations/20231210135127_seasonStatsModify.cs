using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasketballLeagueTracker.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class seasonStatsModify : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OpponentPointsPerGame",
                table: "SeasonStatistics");

            migrationBuilder.DropColumn(
                name: "PointsPerGame",
                table: "SeasonStatistics");

            migrationBuilder.AlterColumn<int>(
                name: "Wins",
                table: "SeasonStatistics",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Losses",
                table: "SeasonStatistics",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "LeaguePoints",
                table: "SeasonStatistics",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "GamesPlayed",
                table: "SeasonStatistics",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<double>(
                name: "OpponentPoints",
                table: "SeasonStatistics",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TeamPoints",
                table: "SeasonStatistics",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OpponentPoints",
                table: "SeasonStatistics");

            migrationBuilder.DropColumn(
                name: "TeamPoints",
                table: "SeasonStatistics");

            migrationBuilder.AlterColumn<int>(
                name: "Wins",
                table: "SeasonStatistics",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Losses",
                table: "SeasonStatistics",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LeaguePoints",
                table: "SeasonStatistics",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GamesPlayed",
                table: "SeasonStatistics",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "OpponentPointsPerGame",
                table: "SeasonStatistics",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PointsPerGame",
                table: "SeasonStatistics",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
