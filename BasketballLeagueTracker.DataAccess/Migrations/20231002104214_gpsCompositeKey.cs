using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasketballLeagueTracker.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class gpsCompositeKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GamePlayerStats",
                table: "GamePlayerStats");

            migrationBuilder.DropIndex(
                name: "IX_GamePlayerStats_PlayerId",
                table: "GamePlayerStats");

            migrationBuilder.DropColumn(
                name: "GamePlayerStatsId",
                table: "GamePlayerStats");

            migrationBuilder.AlterColumn<int>(
                name: "PlayerId",
                table: "GamePlayerStats",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GameId",
                table: "GamePlayerStats",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GamePlayerStats",
                table: "GamePlayerStats",
                columns: new[] { "PlayerId", "GameId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GamePlayerStats",
                table: "GamePlayerStats");

            migrationBuilder.AlterColumn<int>(
                name: "GameId",
                table: "GamePlayerStats",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PlayerId",
                table: "GamePlayerStats",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "GamePlayerStatsId",
                table: "GamePlayerStats",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GamePlayerStats",
                table: "GamePlayerStats",
                column: "GamePlayerStatsId");

            migrationBuilder.CreateIndex(
                name: "IX_GamePlayerStats_PlayerId",
                table: "GamePlayerStats",
                column: "PlayerId");
        }
    }
}
