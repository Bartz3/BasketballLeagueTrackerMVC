using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasketballLeagueTracker.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class isinteam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 1,
                columns: new[] { "IsInTeam", "Positions" },
                values: new object[] { true, (byte)6 });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 2,
                column: "Positions",
                value: (byte)4);

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 3,
                column: "Positions",
                value: (byte)32);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 1,
                columns: new[] { "IsInTeam", "Positions" },
                values: new object[] { false, (byte)3 });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 2,
                column: "Positions",
                value: (byte)2);

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 3,
                column: "Positions",
                value: (byte)16);
        }
    }
}
