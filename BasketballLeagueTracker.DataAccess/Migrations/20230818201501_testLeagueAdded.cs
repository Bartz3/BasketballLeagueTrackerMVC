using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasketballLeagueTracker.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class testLeagueAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Leagues",
                columns: new[] { "LeagueId", "Description", "EndDate", "LeaugeMVPPlayerId", "Logo", "Name", "Season", "StartDate" },
                values: new object[] { 1, "Liga 1 ", null, null, null, "Testowa liga", null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Leagues",
                keyColumn: "LeagueId",
                keyValue: 1);
        }
    }
}
