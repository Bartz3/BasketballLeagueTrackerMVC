using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BasketballLeagueTracker.Migrations
{
    /// <inheritdoc />
    public partial class seedPlayerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Position",
                table: "Players",
                newName: "Positions");

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "PlayerId", "Country", "DateOfBirth", "GamesPlayed", "Height", "IsInTeam", "Name", "Positions", "Surname", "TeamId", "UniformNumber", "Weight" },
                values: new object[,]
                {
                    { 1, null, null, null, null, null, "Bartosz", 3, "Późniewski", null, 10, null },
                    { 2, null, null, null, null, null, "Tom", 2, "Noname", null, 20, null },
                    { 3, null, null, null, null, null, "Test", 16, "Example", null, 30, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 3);

            migrationBuilder.RenameColumn(
                name: "Positions",
                table: "Players",
                newName: "Position");
        }
    }
}
