using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasketballLeagueTracker.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class stadiumAddressAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Stadiums",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Stadiums");
        }
    }
}
